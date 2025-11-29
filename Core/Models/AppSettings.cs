using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaInformacija.Core.Settings
{
    public class AppSettings
    {
        public bool IsWatcherEnabled { get; set; }
        public string TargetFolder { get; set; }
        public string XFolder { get; set; }
        public AppSettings() { 
            IsWatcherEnabled = false;
            TargetFolder = string.Empty;
            XFolder = string.Empty;
        }
    }
}
