using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parserPrimeMarket
{
    class writeToCsv
    {
        public void writeToFile(List<websiteData> websiteData)
        {
            logger.log.Info("Начат процесс записи данных в файл");
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
                        writer.WriteLine($"{item.stockName}\t{item.lastPrice}\t{item.change1D}\t{item.dateTime}\t{item.marketCapitalization}\t" +
                            $"{item.bidVolume}\t{item.askVolume}\t{item.totalVolume}\t{item.totalValue}\t{item.status}");
                        logger.log.Info($"Данные о компании успешно записаны: #{index} - {item.stockName}");
                    }
                }
                logger.log.Info("Данные успешно записаны в файл");
            }
            catch (Exception ex)
            {
                logger.log.Error("Не удалось записать данные", ex);
            }
        }
    }
}
