using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserPrimeMarket
{
    class WebsiteData
    {
        public string StockName { get; set; }
        public string LastPrice { get; set; }
        public string Change1D { get; set; }
        public string DateTime { get; set; }
        public string MarketCapitalization { get; set; }
        public string BidVolume { get; set; }
        public string AskVolume { get; set; }
        public string TotalVolume { get; set; }
        public string TotalValue { get; set; }
        public string Status { get; set; }
    }
}
