using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.ComponentModel;

namespace BmmService
{
    class ProcessWin 
    {
        List<string> ListProcessWins;
        List<string> UserWindows;
        EventLog EventLog;

        public ProcessWin()
        {
            EventLog = new EventLog();
            EventLog.Source = "Processes";
            EventLog.Log = "MbbLog";
            EventLog.WriteEntry("Start watch");
            ListProcessWins = new List<string>(GetCurrentProcesses());

        }

        public void Start()
        {
            while (true)
            {
                Thread.Sleep(2000);
                Check();
            }
        }

        //зпишем в лог, все используемые процессы
        public void Stop()
        {
            string message = ("Stop watch. Totals " + ListProcessWins.Count() + ". ");
            foreach(string processName in ListProcessWins)
            {
                message += processName + ". ";
            }
            EventLog.WriteEntry(message);
        }

        //проверим текущии просессы и сравним с ранее созданными, отметим новые
        private void Check()
        {
            List<string> windowNew;
            Console.Clear();
            if (UserWindows == null || UserWindows.Count == 0)
            {
                UserWindows = GetCurrentProcesses();
                windowNew = UserWindows;
            }
            else
            {
                List<string> processesCurrent = GetCurrentProcesses();
                UserWindows.AddRange(processesCurrent.Except(UserWindows).ToList());
            }
        }


        //получаем процессы с открытыми окнами
        List<string> GetCurrentProcesses()
        {
            List<string> proc = null;
            try
            {
                proc = Process.GetProcesses().Where(e => e.MainWindowTitle.ToString() != "").Select(s => s.MainWindowTitle).ToList();

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message);
            }
            return proc;
        }

    }
}
