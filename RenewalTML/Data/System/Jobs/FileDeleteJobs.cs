using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace RenewalTML.Data.Jobs
{
    public class FileDeleteJobs : HangfireBackgroundWorkerBase
    {
        public FileDeleteJobs()
        {
            RecurringJobId = nameof(FileDeleteJobs);
            CronExpression = "0 * * * *"; // HOURSE INTERVAL - 1
        }

        public override async Task DoWorkAsync()
        {
            Console.WriteLine("test");
        }
    }
}
