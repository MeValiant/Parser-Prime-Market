using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserPrimeMarket
{
    class WriteToCsv
    {
        public void WriteToFile(List<WebsiteData> websiteData)
        {
            Logger.log.Info("Начат процесс записи данных в файл");
            string directoryPath = @"C:\Users\vm962\OneDrive\Desktop";
            string fileName = $"Stocks-prime-market {DateTime.Now.ToString("HH-mm-ss dd-MM-yyyy")}.csv";
            string filePath = Path.Combine(directoryPath, fileName);
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Name\tLast\tChg1D\tDateTime\tMarketCapitalization\tBidVolume\tAskVolume\tTotalVolume\tTotalValue\tStatus");
                    foreach (var (item, index) in websiteData.Select((r,i)=>(r,i)))
                    {
                        writer.WriteLine($"{item.StockName}\t{item.LastPrice}\t{item.Change1D}\t{item.DateTime}\t{item.MarketCapitalization}\t" +
                            $"{item.BidVolume}\t{item.AskVolume}\t{item.TotalVolume}\t{item.TotalValue}\t{item.Status}");
                        Logger.log.Info($"Данные о компании успешно записаны: #{index} - {item.StockName}");
                    }
                }
                Logger.log.Info("Данные успешно записаны в файл");
            }
            catch (Exception ex)
            {
                Logger.log.Error("Не удалось записать данные", ex);
            }
        }
    }
}
