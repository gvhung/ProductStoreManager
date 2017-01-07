using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using StoreManager.FoxEBookProductManager.Core;
using ServiceDataContracts;
using StoreManager.Common;
using StoreManager.LogHandler;

namespace StoreManager.FoxEBookProductManager.JobManager
{
    public enum SchedulerState
    {
        Stopped = 0,
        Running,
        Paused,
    }

    public static class JobScheduler
    {
        public static SchedulerState State { get; set; }

        public static void Start(CancellationToken ct)
        {
            LogManager.WriteToLog(new LogMessage() { Message = "JobScheduler  Running .." });
            while (true)
            {
                Global.SchedulerStopHandle.WaitOne();
                State = SchedulerState.Running;
                if (ct.IsCancellationRequested)
                {
                    State = SchedulerState.Stopped;
                    LogManager.WriteToLog(new LogMessage() { Message = "JobScheduler  Stopped." });
                    ct.ThrowIfCancellationRequested();
                }
                Job j = JobQueue.Dequeue();
                if (j != null)
                {
                    ExceuteJob(j);
                }
                Thread.Sleep(500);
            }
        }

        private static void ExceuteJob(Job j)
        {
            try
            {
                IFoxEbookCrawlerService foxEbookCrawlerService = new FoxEbookCrawlerService();
                j.StartTime = DateTime.Now;
                j.JobStatus = JobStatus.Running;
                j.JobNotifications.OnJobRunning(new JobRunningEventArgs() { Job = j });
                string msg = String.Format("Fetching Products Under Category: {0} In Page #{1}",
                                                                                        j.FoxePage.Category.SEOFriendlyName,
                                                                                        j.FoxePage.PageNo);
                LogManager.WriteToLog(new LogMessage() { Message = msg });

                IList<FoxeProduct> foxeProducts = foxEbookCrawlerService.GetProductsUnderPage(j.FoxePage);

                msg = String.Format("Found '{0}' Products Under Category: {1} In Page #{2}",
                                                                                        foxeProducts.Count,
                                                                                        j.FoxePage.Category.SEOFriendlyName,
                                                                                        j.FoxePage.PageNo);

                LogManager.WriteToLog(new LogMessage() { Message = msg });
                j.JobStatus = JobStatus.Completed;
                j.EndTime = DateTime.Now;
                j.DownloadedProductsCount = foxeProducts.Count;
                j.FoxePage.DownloadedProductsCount = foxeProducts.Count;
                j.JobNotifications.OnJobCompleted(new JobCompletedEventArgs() { Job = j, FoxeProducts = foxeProducts });

                if (JobScheduler.State == SchedulerState.Paused)
                {
                    LogManager.WriteToLog(new LogMessage() { Message = "Scheduler Paused." });
                }
            }
            catch (Exception e)
            {
                j.EndTime = DateTime.Now;
                j.JobStatus = JobStatus.Faulted;
                LogManager.WriteToLog(new LogMessage() { Message = "e.ToString()", Level = LogLevel.Critical });
                j.JobNotifications.OnJobFauled(new JobFauledEventArgs()
                {
                    Job = j,
                    Exception = e
                });
            }
        }
    }
}
