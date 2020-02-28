using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BmmService
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
            eventLog = new EventLog();
            if (!EventLog.SourceExists("MbbSource"))
            {
                EventLog.CreateEventSource(
                "MbbSource", "MbbLog");
            }
            eventLog.Source = "MbbSource";
            eventLog.Log = "MbbLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Start service");
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Stop service");
        }
    }
}
