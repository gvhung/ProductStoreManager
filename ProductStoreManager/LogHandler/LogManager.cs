using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace StoreManager.LogHandler
{
    public static class LogManager
    {
        public static ObservableCollection<LogMessage> LogsList { get; set; }
        public static int SnoCounter { get; set; }

        static LogManager()
        {
            SnoCounter = 0;
            LogsList = new ObservableCollection<LogMessage>();
        }

        public static void Init()
        {
        }
        public static void WriteToLog(LogMessage logMsg)
        {

            System.Windows.Application.Current.Dispatcher.BeginInvoke(
                                                  DispatcherPriority.Background,
                                                  new Action(() =>
                                                  {
                                                      LogsList.Insert(0, logMsg);
                                                  }));

            Console.WriteLine("{0}:{1}", logMsg.TimeStamp, logMsg.Message);
        }
    }
}
