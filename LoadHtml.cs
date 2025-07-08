using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ParserPrimeMarket
{
    class LoadHtml
    {
        private HtmlWeb htmlWeb = new HtmlWeb();
        private readonly string websiteUrl = "https://www.wienerborse.at/en/stocks-prime-market";

        public HtmlDocument HtmlLoad()
        {
            Logger.log.Info($"Загрузка HTML-данных с сайта: {websiteUrl}");
            try
            {
                HtmlDocument _htmlDocument = htmlWeb.Load(websiteUrl);
                Logger.log.Info($"HTML-данные успешно загружены");
                return _htmlDocument;
            }
            catch (Exception ex)
            {
                Logger.log.Error($"Ошибка загрузки HTML-документа", ex);
                return null;
            }
        }
    }
}
