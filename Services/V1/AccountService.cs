using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using WebApi.Entities;
using WebApi.Entities.V1;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.Requests;
using WebApi.Models.Responses;
using System.Security.Cryptography;
using WebApi.Repositories.V1;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApi.Services.V1
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public AccountService(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IUserRepository userRepository
        )
        {
            _mapper = mapper;;
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = _userRepository.FindOne(u => u.Email == model.Email);

            if (account == null || !BC.Verify(model.Password, account.PasswordHash))
                throw new AppException("Email or password is incorrect");
            if (account.Enabled == false)
                throw new AppException("Your account is disabled");


            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            return response;
        }

        public void Register(RegisterRequest model)
        {
            var user = _userRepository.FindOne(x => x.Email == model.Email);
            // validate
            if (user != null)
            {
                // send already registered error to prevent account enumeration
                return;
            }

            // map model to new account object
            var account = _mapper.Map<User>(model);

            switch (account.Role)
            {
                case Role.Admin:
                    account.Role = Role.Admin;
                    break;
                default:
                    break;
            }
            account.Id = Guid.NewGuid().ToString();
            account.Enabled = true;
            account.CreatedAt = DateTime.UtcNow;
            account.VerificationToken = randomTokenString();

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);


            // save account
            _userRepository.InsertOne(account);

            // send email
            //sendVerificationEmail(account, origin);
        }

        //public void ForgotPassword(ForgotPasswordRequest model, string origin)
        //{
        //    var account = _context.Users.SingleOrDefault(x => x.Email == model.Email);

        //    // always return ok response to prevent email enumeration
        //    if (account == null) return;

        //    // create reset token that expires after 1 day
        //    account.ResetToken = randomTokenString();
        //    account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

        //    _context.Users.Update(account);
        //    _context.SaveChanges();

        //    // TODO: send email
        //    var email = new SendEmail
        //    {
        //        ToEmail = account.Email,
        //        Subject = "Forget Password Email",
        //        Payload = new Dictionary<string, string>
        //        {
        //            ["email"] = account.Email,
        //            ["body"] = $"Hi {account.Email}, you told us you forgot your password. If you really did, click here to choose a new one",
        //            ["resetToken"] = $"{account.ResetToken}"
        //        },
        //        TemplateName = "resetPasswordTemplate"
        //    };

        //    //    // TODO: Send CompanyUserCreated an event in kafka (Topic: company_users)
        //    //    _busCompanyUsers.PublishAsync(companyUserCreated.Email, companyUserCreated);
        //    // TODO: Send SendEmail an event in kafka (Topic: emails) for the COInvitation
        //    _busSendEmails.PublishAsync(email.ToEmail, email);
        //}

        //public void VerifyResetToken(VerifyResetTokenRequest model)
        //{
        //    var account = _context.Users.SingleOrDefault(x =>
        //       x.ResetToken == model.Token &&
        //       x.ResetTokenExpires > DateTime.UtcNow);

        //    if (account == null)
        //        throw new AppException("Invalid token");
        //}

        //public void ResetPassword(ResetPasswordRequest model)
        //{
        //    var account = _context.Users.SingleOrDefault(x =>
        //        x.ResetToken == model.Token &&
        //        x.ResetTokenExpires > DateTime.UtcNow);

        //    if (account == null)
        //        throw new AppException("Invalid token");

        //    // update password and remove reset token
        //    account.PasswordHash = BC.HashPassword(model.Password);
        //    account.PasswordReset = DateTime.UtcNow;
        //    account.ResetToken = null;
        //    account.ResetTokenExpires = null;

        //    _context.Users.Update(account);
        //    _context.SaveChanges();
        //}

        private string generateJwtToken(User account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", account.Id.ToString()),
                    new Claim(ClaimTypes.Role, account.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private void removeOldRefreshTokens(User account)
        {
            account.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.CreatedAt.GetValueOrDefault().AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        //public AccountResponse GetAccount(Guid id)
        //{
        //    var account = _userRepository.GetById(id);
        //    return _mapper.Map<AccountResponse>(account);
        //}

        //public AccountResponse UpdateAccount(UpdateAccountRequest request, Guid id)
        //{
        //    var account = _context.Users.Find(id);

        //    // validate
        //    if (account.Email != request.Email && _context.Users.Any(x => x.Email == request.Email))
        //        throw new AppException($"Email '{request.Email}' is already taken");

        //    // copy model to account and save
        //    _mapper.Map(request, account);
        //    account.UpdatedAt = DateTime.UtcNow;
        //    _context.Users.Update(account);
        //    _context.SaveChanges();

        //    var outboxEvent = new Outbox
        //    {
        //        Id = Guid.NewGuid(),
        //        AggregateId = account.Id,
        //        AggregateType = "User",
        //        Type = "UserUpdated",
        //        Payload = JsonConvert.SerializeObject(new CompanyUserCreated
        //        {
        //            Id = account.Id,
        //            Email = account.Email,
        //            LastName = account.LastName,
        //            FirstName = account.FirstName,
        //            Role = account.Role,
        //            EventType = "UserUpdated"
        //        })
        //    };

        //    _context.OutboxEvents.Add(outboxEvent);
        //    _context.SaveChanges();

        //    return _mapper.Map<AccountResponse>(account);
        //}
    }
}
