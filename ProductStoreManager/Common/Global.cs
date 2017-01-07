using StoreManager.DbProductManager.Core;
using StoreManager.FoxEBookProductManager.Core;
using StoreManager.FoxEBookProductManager.JobManager;
using System.Threading.Tasks;
using System.Threading;
using StoreManager.LogHandler;

namespace StoreManager.Common
{
    public enum ImageType
    {
        None,
        Database,
        FoxeWeb
    }

    public static class Global
    {
        public static DbTreeViewNotifications DbTreeViewNotifications { get; set; }
        public static FoxeTreeViewNotifications FoxeTreeViewNotifications { get; set; }
        public static JobNotifications JobNotifications { get; set; }
        public static LogNotifications LogNotifications { get; set; }
        public static CancellationTokenSource CancelTokenSource { get; set; }
        public static CancellationToken CancelToken;
        public static ManualResetEvent SchedulerStopHandle;

        static Global()
        {
            DbTreeViewNotifications = new DbTreeViewNotifications();
            FoxeTreeViewNotifications = new FoxeTreeViewNotifications();
            JobNotifications = new JobNotifications();
            LogNotifications = new LogNotifications();
            SchedulerStopHandle = new ManualResetEvent(false);
        }

        public static void StartScheduler()
        {
            CancelTokenSource = new CancellationTokenSource();
            CancelToken = CancelTokenSource.Token;
            SchedulerStopHandle.Set();
            if (JobScheduler.State == SchedulerState.Stopped)
            {
                JobScheduler.State = SchedulerState.Running;
                new TaskFactory().StartNew(() => { JobScheduler.Start(CancelToken); }, CancelToken);
            }
        }

        public static void StopScheduler()
        {
            if (JobScheduler.State == SchedulerState.Running)
            {
                JobScheduler.State = SchedulerState.Stopped;
                CancelTokenSource.Cancel();
            }
        }

        public static void PauseScheduler()
        {
            LogManager.WriteToLog(new LogMessage() {Message = "Scheduler will be paused after currently executing jobs gets completed" });
            JobScheduler.State = SchedulerState.Paused;
            SchedulerStopHandle.Reset();
        }

        public static void ResumeScheduler()
        {
            LogManager.WriteToLog(new LogMessage() { Message = "Scheduler Resumed .." });
            JobScheduler.State = SchedulerState.Running;
            SchedulerStopHandle.Set();
        }
    }
}
