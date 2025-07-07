using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser__Prime_market_
{
    class WriteToCsv
    {
        public void WriteToFile(List<WebsiteData> _websiteData)
        {
            Logger._log.Info("Начат процесс записи данных в файл");
            string DirectoryPath = @"C:\Users\vm962\OneDrive\Desktop";
            string FileName = $"Stocks-prime-market {DateTime.Now.ToString("HH-mm-ss dd-MM-yyyy")}.csv";
            string FilePath = Path.Combine(DirectoryPath, FileName);
            try
            {
                using (StreamWriter _writer = new StreamWriter(FilePath))
                {
                    _writer.WriteLine("Name;Last;Chg1D;DateTime;MarketCapitalization;BidVolume;AskVolume;TotalVolume;TotalValue;Status");
                    foreach (var (_item, _index) in _websiteData.Select((r,i)=>(r,i)))
                    {
                        _writer.WriteLine($"{_item.Stock_Name};{_item.Last_Price};{_item.Change_1D};{_item.Date_Time};{_item.Market_Capitalization};" +
                            $"{_item.Bid_Volume};{_item.Ask_Volume};{_item.Total_Volume};{_item.Total_Value};{_item.Status}");
                        Logger._log.Info($"Данные о компании успешно записаны: #{_index} - {_item.Stock_Name}");
                    }
                }
                Logger._log.Info("Данные успешно записаны в файл");
            }
            catch (Exception ex)
            {
                Logger._log.Error("Не удалось записать данные", ex);
            }
        }
    }
}
