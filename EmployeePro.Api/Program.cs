using System.Security.Claims;
using System.Text;
using EmployeePro.Bll;
using EmployeePro.Bll.Services;
using EmployeePro.Bll.Services.Authentications;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.Contract.Options;
using EmployeePro.Dal;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Entities.BridgeTables;
using EmployeePro.Dal.Providers;
using EmployeePro.Dal.Providers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RetAil;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.Configure<SecretOptions>(builder.Configuration.GetSection("SecretOptions"));
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICrudProvider<SkillEntity>, Repository<SkillEntity>>();
builder.Services.AddScoped<ICrudProvider<EmployeeEntity>, Repository<EmployeeEntity>>();
builder.Services.AddScoped<ICrudProvider<EducationEntity>, Repository<EducationEntity>>();
builder.Services.AddScoped<ICrudProvider<ExperienceEntity>, Repository<ExperienceEntity>>();
builder.Services.AddScoped<ICrudProvider<DepartmentEntity>, Repository<DepartmentEntity>>();
builder.Services.AddScoped<ICrudProvider<LanguageEntity>, Repository<LanguageEntity>>();
builder.Services.AddScoped<ICrudProvider<EmployeeSkillEntity>, Repository<EmployeeSkillEntity>>();
builder.Services.AddScoped<ICrudProvider<EmployeeLanguageEntity>, Repository<EmployeeLanguageEntity>>();
builder.Services.AddScoped<ICrudProvider<HrEntity>, Repository<HrEntity>>();
builder.Services.AddScoped<ILinkedinService, LinkedinService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmployeeCreator, EmployeeCreator>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEmployeeAuth, EmployeeAuth>();
builder.Services.AddScoped<IHrAuth, HrAuth>();

var secrets = builder.Configuration.GetSection("SecretOptions");

var key = Encoding.ASCII.GetBytes(secrets.GetValue<string>("JWTSecret")!);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization(x => 
    x.AddPolicy("HR", x =>
    {
        x.RequireClaim(ClaimTypes.Actor, "HR");
    }));

builder.Services.AddAuthorization(x => 
    x.AddPolicy("Employee", x =>
    {
        x.RequireClaim(ClaimTypes.Actor, "Employee");
    }));

ConfigureServicesSwagger.ConfigureServices(builder.Services);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();


app.Run();