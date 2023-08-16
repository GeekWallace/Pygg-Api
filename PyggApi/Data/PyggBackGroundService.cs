using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PyggApi.Interfaces.Groups;
using PyggApi.Interfaces.Members;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PyggApi.Data
{
    public class PyggBackGroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private bool _isFirstRun = true;

        public PyggBackGroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Wait for the first run after 2 minutes, then execute the logic
                if (_isFirstRun)
                {
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                    _isFirstRun = false;
                }
                else
                {
                    // Execute the logic every 5 minutes after the first run
                    await ExecuteLogic();
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
            }
        }

        private async Task ExecuteLogic()
        {
            // Create a new scope to resolve scoped dependencies
            using var scope = _scopeFactory.CreateScope();
            var memberGroups = scope.ServiceProvider.GetRequiredService<IGroups>();
            //await memberGroups.GetGroupsDailyRobins();



            // Resolve the IMembers service from the created scope
            var memberBusiness = scope.ServiceProvider.GetRequiredService<IMembers>();

            // Call your PostFines method here with the necessary arguments
            await memberBusiness.PostFines();
            await memberBusiness.UpdateMemberAccountsFromInstallments();
        }
    }
}
