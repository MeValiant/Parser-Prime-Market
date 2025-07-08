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

namespace ParserPrimeMarket
{
    class Parser
    {
        private readonly LoadHtml loadHtml = new LoadHtml();
        private readonly ParseTable parseTable = new ParseTable();

        public List<WebsiteData> ParseData()
        {
            Logger.log.Info("Начало парсинга данных с сайта: Wiener Börse AG - Prime Market");

            try
            {
                HtmlDocument htmlDocument = loadHtml.HtmlLoad();
                return parseTable.TableParse(htmlDocument);
            }
            catch (Exception ex)
            {
                Logger.log.Fatal($"Критическая ошибка при парсинге данных", ex);
                return new List<WebsiteData>();
            }
        }
    }
}