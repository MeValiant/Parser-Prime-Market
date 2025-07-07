using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;

namespace Parser__Prime_market_
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser _parser = new Parser();
            WriteToCsv _writeToCsv = new WriteToCsv();
            _writeToCsv.WriteToFile(_parser.ParseData());
        }
    }
}
