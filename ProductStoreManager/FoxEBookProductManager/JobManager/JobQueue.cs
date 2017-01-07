using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using StoreManager.LogHandler;

namespace StoreManager.FoxEBookProductManager.JobManager
{
    internal static class ConcurrentQueueExtensions
    {
        public static void Clear<T>(this ConcurrentQueue<T> queue)
        {
            T item;
            while (queue.TryDequeue(out item))
            {
                // do nothing
            }
        }

    }
    public static class JobQueue
    {

        private static ConcurrentQueue<Job> _jobQueue { get; set; }
        public static ObservableCollection<Job> JobsSummary { get; set; }

        static JobQueue()
        {
            _jobQueue = new ConcurrentQueue<Job>();
            JobsSummary = new ObservableCollection<Job>();
        }

        public static void Enqueue(Job job)
        {
            job.JobStatus = JobStatus.Queued;
            _jobQueue.Enqueue(job);
            job.JobNotifications.OnJobQueued(new JobQueuedEventArgs() { Job = job });
            JobsSummary.Add(job);
        }

        public static void Clear()
        {
            JobsSummary.Clear();
            _jobQueue.Clear();
            LogManager.WriteToLog(new LogMessage() {Message="Although Cleared, Curently Running Jobs Execute till Completion.." });
        }

        public static void Enqueue(IList<Job> jobs)
        {
            foreach (Job j in jobs)
            {
                Enqueue(j);
            }
        }

        public static bool IsQueueEmpty()
        {
            return _jobQueue.IsEmpty;
        }

        public static Job Dequeue()
        {
            Job j;
            if (_jobQueue.TryDequeue(out j))
            {
                return j;
            }
            return null;
        }
    }
}
