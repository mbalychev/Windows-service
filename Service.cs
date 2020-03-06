using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BmmService
{
    public partial class Service : ServiceBase
    {
        static ProcessWin StartListining;
        public Service()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            StartListining = new ProcessWin();
            Task.Run(()=>StartListining.Start());
        }

        protected override void OnStop()
        {
            StartListining.Stop();
        }
    }

}

