using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using idtiApiService.Service;

namespace idtiApiService.Schedule
{
    public class ScheduledJob : IJob
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<ScheduledJob> logger;
        private IApiService apiService;

        public ScheduledJob(IConfiguration configuration, ILogger<ScheduledJob> logger, IApiService apiService) {
            this.logger = logger;
            this.configuration = configuration;
            this.apiService = apiService;
        }

        public async Task Execute(IJobExecutionContext context) {

            try {

                apiService.pollingDevice();

            } catch (Exception ex) {

                this.logger.LogCritical(ex.Message);
                Console.WriteLine(ex.Message);
                
            }

            await Task.CompletedTask;

        }



    }
}
