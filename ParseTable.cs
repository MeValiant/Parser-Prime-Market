using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using log4net.Config;

namespace Parser__Prime_market_
{
    class ParseTable
    {
        public List<WebsiteData> TableParse(HtmlDocument _htmlDocument)
        {
            Logger._log.Info($"Начало поиска таблицы с данными о акциях");

            var _dataRows = _htmlDocument.DocumentNode.SelectNodes("//table[contains(@class, 'kv-grid-table')]//tbody/tr");

            if (_dataRows == null || !_dataRows.Any())
            {
                Logger._log.Warn("Не найдено ни одной подходящей строки c данными в таблице");
                return new List<WebsiteData>();
            }
            Logger._log.Info($"Найдено {_dataRows.Count} строк удовлетворяющих заданному условию");

            var _receivedData = new List<WebsiteData>();
            int _errorCount = 0;
            int _processedCount = 0;

            foreach (var (_dataRow, _index) in _dataRows.Select((r, i) => (r, i)))
            {
                HtmlNodeCollection _cells = _dataRow.SelectNodes("td");
                if (_cells == null || _cells.Count != 10)
                {
                    Logger._log.Error($"Пропущена {_index} строка с неполными данными - найдено {_cells?.Count ?? 0} ячеек");
                    _errorCount++;
                    continue;
                }
                try
                {
                    WebsiteData _data = RowParse(_cells);
                    Logger._log.Info($"Успешно обработана строка {_index}: {_data.Stock_Name}");
                    _processedCount++;
                    _receivedData.Add(_data);
                }
                catch (Exception ex)
                {
                    Logger._log.Error($"Ошибка при обработке строки: {_index}", ex);
                    _errorCount++;
                    continue;
                }
            }
            Logger._log.Info($"Парсинг завершен. Успешо: {_processedCount} строк, с ошибками: {_errorCount} строк, всего: {_dataRows.Count} строк");
            return _receivedData;
        }

        private WebsiteData RowParse(HtmlNodeCollection _cells)
        {
            return new WebsiteData
            {
                Stock_Name = (_cells[0].SelectSingleNode(".//a")?.InnerText.Trim() ?? ""),
                Last_Price = (_cells[1]?.InnerText.Trim() ?? ""),
                Change_1D = $"{(_cells[2].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]//span")?.InnerText.Trim() ?? "")} / {(_cells[2].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]//span")?.InnerText.Trim() ?? "")}".Trim(),
                Date_Time = $"{(_cells[3]?.SelectSingleNode("./text()[1]")?.InnerText.Trim())} {(_cells[3]?.SelectSingleNode("./text()[2]")?.InnerText.Trim())}",
                Market_Capitalization = (_cells[4]?.InnerText.Trim() ?? ""),
                Bid_Volume = $"{(_cells[5].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]")?.InnerText.Trim() ?? "")} / {(_cells[5].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]")?.InnerText.Trim() ?? "")}".Trim(),
                Ask_Volume = $"{(_cells[6].SelectSingleNode(".//div[contains(@class, 'multi-cell-first')]")?.InnerText.Trim() ?? "")} / {(_cells[6].SelectSingleNode(".//div[contains(@class, 'multi-cell-last')]")?.InnerText.Trim() ?? "")}",
                Total_Volume = (_cells[7]?.InnerText.Trim() ?? ""),
                Total_Value = (_cells[8]?.InnerText.Trim() ?? ""),
                Status = (_cells[9].SelectSingleNode(".//span[contains(@class, 'status')]|.//span")?.InnerText.Trim() ?? "")
            };
        }
    }
}
