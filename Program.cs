using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;

namespace ParserPrimeMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            WriteToCsv writeToCsv = new WriteToCsv();
            writeToCsv.WriteToFile(parser.ParseData());
        }
    }
}