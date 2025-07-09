using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace parserPrimeMarket
{
    class parseTable
    {
        public List<websiteData> tableParse(HtmlDocument htmlDocument)
        {
            logger.log.Info($"Начало поиска таблицы с данными о акциях");

            var dataRows = htmlDocument.DocumentNode.SelectNodes("//table[contains(@class, 'kv-grid-table')]//tbody/tr");

            if (dataRows == null || !dataRows.Any())
            {
                logger.log.Warn("Не найдено ни одной подходящей строки c данными в таблице");
                return new List<websiteData>();
            }
            logger.log.Info($"Найдено {dataRows.Count} строк удовлетворяющих заданному условию");

            var receivedData = new List<websiteData>();
            int errorCount = 0;
            int processedCount = 0;

            foreach (var (dataRow, index) in dataRows.Select((r, i) => (r, i)))
            {
                HtmlNodeCollection cells = dataRow.SelectNodes("td");
                if (cells == null || cells.Count != 10)
                {
                    logger.log.Error($"Пропущена {index} строка с неполными данными - найдено {cells?.Count ?? 0} ячеек");
                    errorCount++;
                    continue;
                }
                try
                {
                    websiteData data = rowParse(cells);
                    logger.log.Info($"Успешно обработана строка {index}: {data.stockName}");
                    processedCount++;
                    receivedData.Add(data);
                }
                catch (Exception ex)
                {
                    logger.log.Error($"Ошибка при обработке строки: {index}", ex);
                    errorCount++;
                    continue;
                }
            }
            logger.log.Info($"Парсинг завершен. Успешо: {processedCount} строк, с ошибками: {errorCount} строк, всего: {dataRows.Count} строк");
            return receivedData;
        }

        private websiteData rowParse(HtmlNodeCollection cells)
        {
            return new websiteData
            {
                stockName = (cells[0].SelectSingleNode(".//a")?.InnerText.Trim() ?? ""),
                lastPrice = (cells[1]?.InnerText.Trim() ?? ""),
                change1D = $"{(cells[2].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]//span")?.InnerText.Trim() ?? "")} / {(cells[2].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]//span")?.InnerText.Trim() ?? "")}".Trim(),
                dateTime = $"{(cells[3]?.SelectSingleNode("./text()[1]")?.InnerText.Trim())} {(cells[3]?.SelectSingleNode("./text()[2]")?.InnerText.Trim())}",
                marketCapitalization = (cells[4]?.InnerText.Trim() ?? ""),
                bidVolume = $"{(cells[5].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]")?.InnerText.Trim() ?? "")} / {(cells[5].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]")?.InnerText.Trim() ?? "")}".Trim(),
                askVolume = $"{(cells[6].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]")?.InnerText.Trim() ?? "")} / {(cells[6].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]")?.InnerText.Trim() ?? "")}",
                totalVolume = (cells[7]?.InnerText.Trim() ?? ""),
                totalValue = (cells[8]?.InnerText.Trim() ?? ""),
                status = (cells[9].SelectSingleNode(".//span[contains(@class, 'status')]|.//span")?.InnerText.Trim() ?? "")
            };
        }
    }
}
