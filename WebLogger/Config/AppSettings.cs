using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLogger.Config
{
    public class LoggerSettings
    {
        public RavenDBSink RavenDBSink { get; set; }
        public string FilePath { get; set; }
        public string SeqUrl { get; set; }
    }


    public class RavenDBSink
    {
        public string[] Urls { get; set; }
        public string Database { get; set; }
    }
}
