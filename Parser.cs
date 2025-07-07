using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using log4net;
using log4net.Config;
using static log4net.Appender.RollingFileAppender;

namespace Parser__Prime_market_
{
    class Parser
    {
        private readonly LoadHtml _loadHtml = new LoadHtml();
        private readonly ParseTable _parseTable = new ParseTable();

        public List<WebsiteData> ParseData()
        {
            Logger._log.Info("Начало парсинга данных с сайта: Wiener Börse AG - Prime Market");

            try
            {
                HtmlDocument _htmlDocument = _loadHtml.HtmlLoad();
                return _parseTable.TableParse(_htmlDocument);
            }
            catch (Exception ex)
            {
                Logger._log.Fatal($"Критическая ошибка при парсинге данных", ex);
                return new List<WebsiteData>();
            }
        }
    }
}