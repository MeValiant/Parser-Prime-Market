using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Parser__Prime_market_
{
    class Logger
    {
        public static readonly ILog _log = LogManager.GetLogger(typeof(Logger));
        static Logger()
        {
            XmlConfigurator.Configure();
        }
    }
}
