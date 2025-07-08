using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using log4net.Config;

namespace ParserPrimeMarket
{
    class ParseTable
    {
        public List<WebsiteData> TableParse(HtmlDocument htmlDocument)
        {
            Logger.log.Info($"Начало поиска таблицы с данными о акциях");

            var dataRows = htmlDocument.DocumentNode.SelectNodes("//table[contains(@class, 'kv-grid-table')]//tbody/tr");

            if (dataRows == null || !dataRows.Any())
            {
                Logger.log.Warn("Не найдено ни одной подходящей строки c данными в таблице");
                return new List<WebsiteData>();
            }
            Logger.log.Info($"Найдено {dataRows.Count} строк удовлетворяющих заданному условию");

            var receivedData = new List<WebsiteData>();
            int errorCount = 0;
            int processedCount = 0;

            foreach (var (dataRow, index) in dataRows.Select((r, i) => (r, i)))
            {
                HtmlNodeCollection cells = dataRow.SelectNodes("td");
                if (cells == null || cells.Count != 10)
                {
                    Logger.log.Error($"Пропущена {index} строка с неполными данными - найдено {cells?.Count ?? 0} ячеек");
                    errorCount++;
                    continue;
                }
                try
                {
                    WebsiteData data = RowParse(cells);
                    Logger.log.Info($"Успешно обработана строка {index}: {data.StockName}");
                    processedCount++;
                    receivedData.Add(data);
                }
                catch (Exception ex)
                {
                    Logger.log.Error($"Ошибка при обработке строки: {index}", ex);
                    errorCount++;
                    continue;
                }
            }
            Logger.log.Info($"Парсинг завершен. Успешо: {processedCount} строк, с ошибками: {errorCount} строк, всего: {dataRows.Count} строк");
            return receivedData;
        }

        private WebsiteData RowParse(HtmlNodeCollection cells)
        {
            return new WebsiteData
            {
                StockName = (cells[0].SelectSingleNode(".//a")?.InnerText.Trim() ?? ""),
                LastPrice = (cells[1]?.InnerText.Trim() ?? ""),
                Change1D = $"{(cells[2].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]//span")?.InnerText.Trim() ?? "")} / {(cells[2].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]//span")?.InnerText.Trim() ?? "")}".Trim(),
                DateTime = $"{(cells[3]?.SelectSingleNode("./text()[1]")?.InnerText.Trim())} {(cells[3]?.SelectSingleNode("./text()[2]")?.InnerText.Trim())}",
                MarketCapitalization = (cells[4]?.InnerText.Trim() ?? ""),
                BidVolume = $"{(cells[5].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]")?.InnerText.Trim() ?? "")} / {(cells[5].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]")?.InnerText.Trim() ?? "")}".Trim(),
                AskVolume = $"{(cells[6].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]")?.InnerText.Trim() ?? "")} / {(cells[6].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]")?.InnerText.Trim() ?? "")}",
                TotalVolume = (cells[7]?.InnerText.Trim() ?? ""),
                TotalValue = (cells[8]?.InnerText.Trim() ?? ""),
                Status = (cells[9].SelectSingleNode(".//span[contains(@class, 'status')]|.//span")?.InnerText.Trim() ?? "")
            };
        }
    }
}
