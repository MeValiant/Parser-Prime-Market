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

namespace parserPrimeMarket
{
    class parser
    {
        private readonly loadHtml loadHtml = new loadHtml();
        private readonly parseTable parseTable = new parseTable();

        public List<websiteData> parseData()
        {
            logger.log.Info("Начало парсинга данных с сайта: Wiener Börse AG - Prime Market");

            try
            {
                HtmlDocument htmlDocument = loadHtml.htmlLoad();
                return parseTable.tableParse(htmlDocument);
            }
            catch (Exception ex)
            {
                logger.log.Fatal($"Критическая ошибка при парсинге данных", ex);
                return new List<websiteData>();
            }
        }
    }
}