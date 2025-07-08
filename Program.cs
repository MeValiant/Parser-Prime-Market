using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;

namespace parserPrimeMarket
{
    class program
    {
        static void Main(string[] args)
        {
            parser parser = new parser();
            writeToCsv writeToCsv = new writeToCsv();
            writeToCsv.writeToFile(parser.parseData());
        }
    }
}