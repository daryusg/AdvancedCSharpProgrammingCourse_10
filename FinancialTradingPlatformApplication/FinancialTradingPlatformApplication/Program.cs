//Part 18 - Async / Await Task - Task.Run()
//https://github.com/GavinLonDigital/FinancialTradingPlatformApplication/blob/master/FinancialTradingPlatformApplication/Program.cs

namespace FinancialTradingPlatformApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Method name: {nameof(Main)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");

            StockMarketTechnicalAnalysisData stockMarketTechnicalAnalysisData = new StockMarketTechnicalAnalysisData("STKZA", new DateTime(2016,9,5), new DateTime(2026,5,9));

            DateTime dateTimeStart = DateTime.Now;

            //call methods synchronously
            //decimal[] data1 = stockMarketTechnicalAnalysisData.GetOpeningPrices();
            //decimal[] data2 = stockMarketTechnicalAnalysisData.GetClosingPrices();
            //decimal[] data3 = stockMarketTechnicalAnalysisData.GetPriceHighs();
            //decimal[] data4 = stockMarketTechnicalAnalysisData.GetPriceLows();
            //decimal[] data5 = stockMarketTechnicalAnalysisData.CalculateStockastics();
            //decimal[] data6 = stockMarketTechnicalAnalysisData.CalculateFastMovingAverage();
            //decimal[] data7 = stockMarketTechnicalAnalysisData.CalculateSlowMovingAverage();
            //decimal[] data8 = stockMarketTechnicalAnalysisData.CalculateUpperBoundBollingerBand();
            //decimal[] data9 = stockMarketTechnicalAnalysisData.CalculateLowerBoundBollingerBand();

            //call methods asynchronously
            List<Task<decimal[]>> tasks = new List<Task<decimal[]>>();
            Task<decimal[]> getOpeningPricesTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetOpeningPrices());
            Task<decimal[]> getClosingPricesTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetClosingPrices());
            Task<decimal[]> getPriceHighsTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetPriceHighs());
            Task<decimal[]> getPriceLowsTask = Task.Run(() => stockMarketTechnicalAnalysisData.GetPriceLows());
            Task<decimal[]> getStockasticsTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateStockastics());
            Task<decimal[]> getFastMovingAverageTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateFastMovingAverage());
            Task<decimal[]> getSlowMovingAverageTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateSlowMovingAverage());
            Task<decimal[]> getUpperBoundBollingerBandTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateUpperBoundBollingerBand());
            Task<decimal[]> getLowerBoundBollingerBandTask = Task.Run(() => stockMarketTechnicalAnalysisData.CalculateLowerBoundBollingerBand());

            tasks.Add(getOpeningPricesTask);
            tasks.Add(getClosingPricesTask);
            tasks.Add(getPriceHighsTask);
            tasks.Add(getPriceLowsTask);
            tasks.Add(getStockasticsTask);
            tasks.Add(getFastMovingAverageTask);
            tasks.Add(getSlowMovingAverageTask);
            tasks.Add(getUpperBoundBollingerBandTask);
            tasks.Add(getLowerBoundBollingerBandTask);

            //couldn't find https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitall
            //nearest(?): https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitall?view=net-10.0
            Task.WaitAll(tasks.ToArray());

            decimal[] data1 = tasks[0].Result;
            decimal[] data2 = tasks[1].Result;
            decimal[] data3 = tasks[2].Result;
            decimal[] data4 = tasks[3].Result;
            decimal[] data5 = tasks[4].Result;
            decimal[] data6 = tasks[5].Result;
            decimal[] data7 = tasks[6].Result;
            decimal[] data8 = tasks[7].Result;
            decimal[] data9 = tasks[8].Result;

            DateTime dateTimeEnd = DateTime.Now;
            TimeSpan timeSpan = dateTimeEnd.Subtract(dateTimeStart);
            Console.WriteLine($"Execution time: {timeSpan.Seconds} second{(timeSpan.Seconds == 1 ? "" : "s")}.");

            DisplayDataOnChart(data1, data2, data3, data4, data5, data6, data7, data8, data9);

            Console.ReadKey();
        }

        public static void DisplayDataOnChart(decimal[] data1, decimal[] data2, decimal[] data3, decimal[] data4, decimal[] data5, decimal[] data6, decimal[] data7, decimal[] data8, decimal[] data9)
        {
            // Implementation for displaying data on chart
            Console.WriteLine("Data displayed on the chart.");
        }
    }

    public class StockMarketTechnicalAnalysisData
    {
        int sleep1s = 1000, sleep5s = 5000, sleep6s = 6000, sleep7s = 7000, sleep10s = 10000; // Simulated delay in milliseconds

        public StockMarketTechnicalAnalysisData(string stockSymbol, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            //code here gets stock market data from remote server
        }

        public decimal[] GetOpeningPrices()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(GetOpeningPrices)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep1s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        public decimal[] GetClosingPrices()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(GetClosingPrices)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep1s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        public decimal[] GetPriceHighs()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(GetPriceHighs)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep1s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        public decimal[] GetPriceLows()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(GetPriceLows)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep1s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        public decimal[] CalculateStockastics()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(CalculateStockastics)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep10s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        public decimal[] CalculateFastMovingAverage()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(CalculateFastMovingAverage)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep6s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        public decimal[] CalculateSlowMovingAverage()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(CalculateSlowMovingAverage)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep7s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        public decimal[] CalculateUpperBoundBollingerBand()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(CalculateUpperBoundBollingerBand)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep5s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        public decimal[] CalculateLowerBoundBollingerBand()
        {
            decimal[] data;
            Console.WriteLine($"Method name: {nameof(CalculateLowerBoundBollingerBand)}, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(sleep5s); // Simulate a delay in data retrieval
            data = GeneratePrices(); // Simulated data
            return data;
        }

        private decimal[] GeneratePrices()
        {
            Random random = new Random();
            decimal[] prices = new decimal[10];

            for (int i = 0; i < prices.Length; i++)
            {
                prices[i] = random.Next(100, 30001) / 100m;
            }

            return prices;
        }

    }
}
