using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using ServiceDataContracts;
using StoreManager.Common;
using System.ComponentModel;

namespace StoreManager.FoxEBookProductManager.JobManager
{
    public class Job : INotifyPropertyChanged
    {
        public JobNotifications JobNotifications { get; set; }
        public FoxePage FoxePage { get; set; }

        private JobStatus jobStatus;
        public JobStatus JobStatus
        {
            get
            {
                return this.jobStatus;
            }
            set
            {
                if (this.jobStatus != value)
                {
                    this.jobStatus = value;
                    this.NotifyPropertyChanged("JobStatus");
                }
            }
        }

        private JobName jobName;
        public JobName JobName
        {
            get
            {
                return this.jobName;
            }
            set
            {
                if (this.jobName != value)
                {
                    this.jobName = value;
                    this.NotifyPropertyChanged("JobName");
                }
            }
        }

        private Guid id;
        public Guid Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.NotifyPropertyChanged("Id");
                }
            }
        }

        private DateTime startTime;
        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                if (this.startTime != value)
                {
                    this.startTime = value;
                    this.NotifyPropertyChanged("StartTime");
                }
            }
        }

        private DateTime endTime;
        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                if (this.endTime != value)
                {
                    this.endTime = value;
                    this.NotifyPropertyChanged("EndTime");
                    TimeTaken = StartTime - EndTime;
                }
            }
        }

        public string Category
        {
            get
            {
                return this.FoxePage.Category.SEOFriendlyName;
            }
        }

        public string PageUrl
        {
            get
            {
                return "Page Url Not Set";
            }
        }

        public int PageNo
        {
            get
            {
                return this.FoxePage.PageNo;
            }
        }

        private int downloadableProductsCount;
        public int DownloadableProductsCount
        {
            get
            {
                return downloadableProductsCount;
            }

            set
            {
                if (this.downloadableProductsCount != value)
                {
                    this.downloadableProductsCount = value;
                    this.NotifyPropertyChanged("DownloadableProductsCount");
                }
            }
        }

        private int downloadedProductsCount;
        public int DownloadedProductsCount
        {
            get
            {
                return downloadedProductsCount;
            }

            set
            {
                if (this.downloadedProductsCount != value)
                {
                    this.downloadedProductsCount = value;
                    this.NotifyPropertyChanged("DownloadedProductsCount");
                }
            }
        }

        private TimeSpan timeTaken;
        public TimeSpan TimeTaken
        {
            get
            {
                return DateTime.Now - StartTime;
            }
            set
            {
                if (this.timeTaken != value)
                {
                    this.timeTaken = value;
                    this.NotifyPropertyChanged("TimeTaken");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public Job(FoxePage foxePage)
        {
            JobNotifications = Global.JobNotifications;
            Id = new Guid();
            StartTime = DateTime.Now;
            TimeTaken = TimeSpan.Zero;
            FoxePage = foxePage;
            JobStatus = JobStatus.Created;
            JobNotifications.OnJobCreated(new JobCreatedEventArgs() { Job = this });
        }
    }
}
