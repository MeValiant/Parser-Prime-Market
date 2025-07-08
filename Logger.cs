using log4net;
using log4net.Config;

namespace parserPrimeMarket
{
    class logger
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(logger));
        static logger()
        {
            XmlConfigurator.Configure();
        }
    }
}
