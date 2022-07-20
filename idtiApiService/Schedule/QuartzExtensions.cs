using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace idtiApiService.Schedule
{
    public static class QuartzExtensions
    {


        public static IConfigurationRoot configuration { get; set; }

        public static void AddQuartz(this IServiceCollection services, Type jobType)
        {

            var builder = new ConfigurationBuilder() 
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

            configuration = builder.Build();

            int pollingDelay = int.Parse(configuration["CommInterval:pollingDelay"]);

            //Console.WriteLine($"polling delay : ({pollingDelay} seconds) ");

            services.Add(new ServiceDescriptor(typeof(IJob), jobType, ServiceLifetime.Transient));
            services.AddSingleton<IJobFactory, ScheduledJobFactory>();
            services.AddSingleton<IJobDetail>(provider =>
            {
                return JobBuilder.Create<ScheduledJob>()
                  .WithIdentity("Sample.job", "group1")
                  .Build();
            });

            services.AddSingleton<ITrigger>(provider =>
            {
                return TriggerBuilder.Create()
                .WithIdentity($"Sample.trigger", "group1")
                .StartNow()
                .WithSimpleSchedule
                 (s =>
                    s.WithInterval(TimeSpan.FromSeconds(pollingDelay))
                    .RepeatForever()
                 )
                 .Build();
            });

            services.AddSingleton<IScheduler>(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });

        }


        public static void UseQuartz(this IApplicationBuilder app) {
            app.ApplicationServices.GetService<IScheduler>()
                .ScheduleJob(app.ApplicationServices.GetService<IJobDetail>(),
                app.ApplicationServices.GetService<ITrigger>()
                );
        }





    }
}
