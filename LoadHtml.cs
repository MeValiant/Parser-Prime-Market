using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Parser__Prime_market_
{
    class LoadHtml
    {
        private HtmlWeb _htmlWeb = new HtmlWeb();
        private readonly string _websiteUrl = "https://www.wienerborse.at/en/stocks-prime-market";

        public HtmlDocument HtmlLoad()
        {
            Logger._log.Info($"Загрузка HTML-данных с сайта: {_websiteUrl}");
            try
            {
                HtmlDocument _htmlDocument = _htmlWeb.Load(_websiteUrl);
                Logger._log.Info($"HTML-данные успешно загружены");
                return _htmlDocument;
            }
            catch (Exception ex)
            {
                Logger._log.Error($"Ошибка загрузки HTML-документа", ex);
                return null;
            }
        }
    }
}
