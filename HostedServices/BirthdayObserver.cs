using EmployeePro.Bll.Services.Interfaces;
using EmployeePro.Dal.Entities;
using EmployeePro.Dal.Providers.Interfaces;

namespace HostedServices;

public class BirthdayObserver : BackgroundService
{
    private readonly IServiceProvider _provider;

    public BirthdayObserver(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateScope();
            var provider = scope.ServiceProvider.GetRequiredService<ICrudProvider<EmployeeEntity>>();
            var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

            var employeesEntities = await provider.GetAll();

            var emails = employeesEntities
                .Where(x => x.Birthday != null
                            && x.Birthday.Value.Date == DateTime.UtcNow.Date
                            && IsInWorkingHours(DateTime.UtcNow))
                .Select(x => x.Email)
                .ToList();

            foreach (var email in emails)
            {
                await emailSender.SendHappyBirthday(email);
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }

            await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
        }
    }

    private bool IsInWorkingHours(DateTime date)
    {
        var time = TimeOnly.FromDateTime(date);
        var workStarts = new TimeOnly(3, 0);
        var workStops = new TimeOnly(12, 0);
        return time >= workStarts && time <= workStops;
    }
}