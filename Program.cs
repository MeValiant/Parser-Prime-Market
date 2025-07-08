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