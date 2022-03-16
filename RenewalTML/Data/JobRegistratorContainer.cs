using RenewalTML.Data.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;

namespace RenewalTML.Data
{
    public static class JobRegistratorContainer
    {
        public static void RegisterContainerJobsApplication(this ApplicationInitializationContext context)
        {
            /* SYSTEM JOBS */

            context.AddBackgroundWorkerAsync<FileDeleteJobs>();
        }
    }
}
