using WebApi.Models;
using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.Services.V1
{
    public interface IAccountService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        void Register(RegisterRequest model);
        //AccountResponse GetAccount(Guid id);
        //AccountResponse UpdateAccount(UpdateAccountRequest request, Guid id);
        //void ForgotPassword(ForgotPasswordRequest model, string origin);
        //void VerifyResetToken(VerifyResetTokenRequest model);
        //void ResetPassword(ResetPasswordRequest model);
    }
}
