using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StockMarketComponent;

//Part 19 - Async / Await Task - Best Practices
//https://github.com/GavinLonDigital/FinancialTradingPlatformUWP/blob/master/FinancialTradingPlatformUWP/MainPage.xaml.cs

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FinancialTradingPlatformUWP
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        string _data = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddListItem(string text)
        {
            ListViewItem listViewItem = new ListViewItem();
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            listViewItem.Content = textBlock;
            lvwOutput.Items.Add(listViewItem);

        }

        private void btnFastLocalOperation_Click(object sender, RoutedEventArgs e)
        {
            AddListItem($"Fast Local Operation Completed - ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        }

        private async void btnCPUBoundOperations_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(_data))
            {
                StockMarketData stockMarketData = new StockMarketData();
                _data = await stockMarketData.GetStockDataAsync();
            }

            StockMarketDataAnalysis stockMarketDataAnalysis = new StockMarketDataAnalysis(_data);

            //string result = await Task.Run(() => stockMarketDataAnalysis.CalculateStockastics());
            //AddListItem(result);

            List<Task<string>> tasks = new List<Task<string>>();

            tasks.Add(Task.Run(() => stockMarketDataAnalysis.CalculateFastMovingAverage()));
            tasks.Add(Task.Run(() => stockMarketDataAnalysis.CalculateSlowMovingAverage()));
            tasks.Add(Task.Run(() => stockMarketDataAnalysis.CalculateStockastics()));
            tasks.Add(Task.Run(() => stockMarketDataAnalysis.CalculateBollingerBands()));

            //Task.WaitAll(tasks.ToArray());
            await Task.WhenAll(tasks.ToArray());
            //await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);

            AddListItem(tasks[0].Result);
            AddListItem(tasks[1].Result);
            AddListItem(tasks[2].Result);
            AddListItem(tasks[3].Result);

            DisplayIndicatorsOnChart(tasks[0].Result, tasks[1].Result, tasks[2].Result, tasks[3].Result);
            SaveIndicatorDataToFile(tasks[0].Result, tasks[1].Result, tasks[2].Result, tasks[3].Result);
        }
        private void SaveIndicatorDataToFile(string data1, string data2, string data3, string data4)
        {
            //Code goes here to save indicator data to file
        }

        private void DisplayIndicatorsOnChart(string data1, string data2, string data3, string data4)
        {
            //Code goes here to display indicator data on chart
        }
    }
}
