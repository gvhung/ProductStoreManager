using System;
using StoreManager.FoxEBookProductManager.AppViews;
using System.Windows.Controls;
using ServiceDataContracts;

namespace StoreManager.LogHandler
{
    public class LogNotificationsEventArgs : EventArgs
    {
        public LogMessage LogMessage { get; set; }
    }

    public class LogNotifications
    {
        public event EventHandler<LogNotificationsEventArgs> AppLogMessage;
        public event EventHandler<LogNotificationsEventArgs> TxtLogMessage;
        public event EventHandler<LogNotificationsEventArgs> HtmlLogMessage;

        public LogNotifications()
        {

        }

        public void OnLogMessage(LogNotificationsEventArgs e)
        {
            if (AppLogMessage != null)
            {
                AppLogMessage(this, e);
            }

            if (TxtLogMessage != null)
            {
                TxtLogMessage(this, e);
            }

            if (HtmlLogMessage != null)
            {
                HtmlLogMessage(this, e);
            }
        }
    }
}
