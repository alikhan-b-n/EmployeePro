using System.Text;
using EmployeePro.Bll;
using EmployeePro.Bll.Services;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.Dal;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Entities.BridgeTables;
using EmployeePro.Dal.Providers;
using EmployeePro.Dal.Providers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddScoped<ILinkedinService, LinkedinService>();
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();
builder.Services.AddScoped<IEmployeeCreatorAndUpdater, EmployeeCreator>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();

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