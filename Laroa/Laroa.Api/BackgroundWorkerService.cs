using System.Reflection;
using System.Threading;
using Laroa.Application;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Laroa.Domain.Interfaces.Services;
using Laroa.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class BackgroundWorkerService : BackgroundService
{
    readonly ILogger<BackgroundWorkerService> _logger;
    private readonly IMailService _mailService;
    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IMailService mailService)
    {
        _logger = logger;
        _mailService = mailService;
    }


    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Backround worker has started");
        var request = new MailRequest
        {
            ToEmail = "roxananilvan08@gmail.com",
            Subject = "Application status",
            Body = "Evrething is working fine"
        };
        try
        {
            await _mailService.SendEmailAsync(request);
        }
        catch (Exception exception)
        {
            throw;
        }
        await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
    }
}