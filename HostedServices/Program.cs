using EmployeePro.Bll;
using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.Dal;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers;
using EmployeePro.Dal.Providers.Interfaces;
using HostedServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddScoped<ICrudProvider<EmployeeEntity>, Repository<EmployeeEntity>>();
builder.Services.AddScoped<IEmailSender, EmailSender>();



builder.Services.AddHostedService<BirthdayObserver>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();