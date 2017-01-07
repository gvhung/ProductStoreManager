using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace StoreManager.LogHandler
{
    public enum LogLevel
    {
        Information = 0,
        Warning,
        Error,
        Critical
    }

    public class LogMessage : INotifyPropertyChanged
    {
        public LogMessage()
        {
            Level = LogLevel.Information;
            TimeStamp = DateTime.Now;
            Sno = ++LogManager.SnoCounter;
        }

        private int sno;
        public int Sno
        {
            get
            {
                return this.sno;
            }
            set
            {
                if (this.sno != value)
                {
                    this.sno = value;
                    this.NotifyPropertyChanged("Sno");
                }
            }
        }

        private DateTime timeStamp;
        public DateTime TimeStamp
        {
            get
            {
                return this.timeStamp;
            }
            set
            {
                if (this.timeStamp != value)
                {
                    this.timeStamp = value;
                    this.NotifyPropertyChanged("TimeStamp");
                }
            }
        }


        private LogLevel level;
        public LogLevel Level
        {
            get
            {
                return this.level;
            }
            set
            {
                if (this.level != value)
                {
                    this.level = value;
                    this.NotifyPropertyChanged("Level");
                }
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    this.NotifyPropertyChanged("Message");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
