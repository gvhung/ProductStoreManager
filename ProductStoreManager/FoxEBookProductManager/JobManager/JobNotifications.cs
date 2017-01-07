using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceDataContracts;

namespace StoreManager.FoxEBookProductManager.JobManager
{
    public enum JobStatus
    {
        Created=0,
        Queued,
        Running,
        Completed,
        Faulted
    }

    public enum JobName
    {
        Download = 0
    }

    public class JobCreatedEventArgs : EventArgs
    {
        public Job Job { get; set; }
    }

    public class JobQueuedEventArgs : EventArgs
    {
        public Job Job { get; set; }
    }

    public class JobRunningEventArgs : EventArgs
    {
        public Job Job { get; set; }
    }

    public class JobCompletedEventArgs : EventArgs
    {
        public Job Job { get; set; }
        public IList<FoxeProduct> FoxeProducts { get; set; }
    }

    public class JobFauledEventArgs : EventArgs
    {
        public Job Job { get; set; }
        public Exception Exception { get; set; }
    }

    public class JobNotifications
    {
        public event EventHandler<JobCreatedEventArgs> JobCreated;
        public event EventHandler<JobQueuedEventArgs> JobQueued;
        public event EventHandler<JobRunningEventArgs> JobRunning;
        public event EventHandler<JobCompletedEventArgs> JobCompleted;
        public event EventHandler<JobFauledEventArgs> JobFauled;

        public JobNotifications()
        {

        }

        public void OnJobCreated(JobCreatedEventArgs e)
        {
            if (JobCreated != null)
            {
                JobCreated(this, e);
            }
        }

        public void OnJobQueued(JobQueuedEventArgs e)
        {
            if (JobQueued != null)
            {
                JobQueued(this, e);
            }
        }

        public void OnJobRunning(JobRunningEventArgs e)
        {
            if (JobRunning != null)
            {
                JobRunning(this, e);
            }
        }

        public void OnJobCompleted(JobCompletedEventArgs e)
        {
            if (JobCompleted != null)
            {
                JobCompleted(this, e);
            }
        }
        public void OnJobFauled(JobFauledEventArgs e)
        {
            if (JobFauled != null)
            {
                JobFauled(this, e);
            }
        }
    }
}
