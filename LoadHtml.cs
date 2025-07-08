using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace parserPrimeMarket
{
    class loadHtml
    {
        private HtmlWeb htmlWeb = new HtmlWeb();
        private readonly string websiteUrl = "https://www.wienerborse.at/en/stocks-prime-market";

        public HtmlDocument htmlLoad()
        {
            logger.log.Info($"Загрузка HTML-данных с сайта: {websiteUrl}");
            try
            {
                HtmlDocument htmlDocument = htmlWeb.Load(websiteUrl);
                logger.log.Info($"HTML-данные успешно загружены");
                return htmlDocument;
            }
            catch (Exception ex)
            {
                logger.log.Error($"Ошибка загрузки HTML-документа", ex);
                return null;
            }
        }
    }
}
