using Api.Application.Interface;
using Api.Application.Services;
using Api.Common.AutoMapper;
using Api.Common.Utils;
using Api.Infraestructure.Injection;
using Api.Infraestructure.Interface;
using Api.Infraestructure.Repository;
using Api.Presentation.Middleware;
using AutoMapper.EquivalencyExpression;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");
var config = configuration.Build();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

var secret = config.GetValue<string>("KeySecret");
secret = secret != null ? secret : "";
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.RequireHttpsMetadata = false;
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
//Agregar auto mapper
builder.Services.AddAutoMapper(options =>
{
    options.AddCollectionMappers();
    options.AddProfile(new BaseAutoMapper());
});

builder.Services.AddDbContext(builder.Configuration, false);

// ===================== Inyección de dependencias
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IEmployeesServices, EmployeesServices>();
builder.Services.AddSingleton(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
builder.Services.AddSingleton<LanguageSettings>();

// ===================== Agregar los claims al sistema
var configurationClaims = builder.Configuration.GetSection("TokenSystem").Get<List<ClassToken>>();
builder.Services.AddAuthorization(options =>
{
    foreach (var item in configurationClaims ?? new List<ClassToken>())
    {
        options.AddPolicy(item.Token.ToString(), policy => policy.RequireClaim("Token", item.Token.ToString()));
    }
});

// ===================== Agregar cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1", builder =>
    {
        builder.WithOrigins("http://localhost:4200","https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

WebApplication app = builder.Build();

app.UseCors("Policy1");
app.UseLanguageMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{ }