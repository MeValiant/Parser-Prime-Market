using System;
using System.Collections.Generic;
using HtmlAgilityPack;

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