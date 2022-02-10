using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Datas.DbConfigs;
using WebApi.Helpers;
using WebApi.Mappings;
using WebApi.Middlewares;
using WebApi.Repositories;
using WebApi.Repositories.V1;
using WebApi.Services;
using WebApi.Services.V1;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    services.AddSwaggerGen();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    // configure mogodb connection string
    services.Configure<DatabaseConfigurations>(builder.Configuration.GetSection("DatabaseConfigurations"));
    
    services.AddSingleton<IDatabaseConfigurations>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<DatabaseConfigurations>>().Value);

    // contract to entity (vise versa) mapper configuration
    services.AddAutoMapper(typeof(AutoMapperProfile));

    // configure DI for application repositories
    services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    services.AddTransient<IDummyRepository, DummyRepository>();
    services.AddTransient<IUserRepository, UserRepository>();

    // configure DI for application services
    services.AddScoped<IDummyService, DummyService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddSingleton<IUriService>(provider =>
    {
        var accessor = provider.GetRequiredService<IHttpContextAccessor>();
        var request = accessor.HttpContext.Request;
        var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
        return new UriService(absoluteUri);
    });
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapControllers();
}

app.Run("http://localhost:4000");