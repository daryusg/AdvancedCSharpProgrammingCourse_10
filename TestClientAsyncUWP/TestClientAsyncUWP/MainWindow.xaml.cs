using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Net.Http;

//Part 17 - Async / Await Task - Introduction
//https://github.com/GavinLonDigital/TestClientAsyncUWP/blob/master/TestClientAsyncUWP/MainPage.xaml.cs

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestClientAsyncUWP
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        int _localOperationCounter = 0;
        int _webAPIOperationCounter = 0;
        
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

        private void btnLocalOperation_Click(object sender, RoutedEventArgs e)
        {
            _localOperationCounter++;
            AddListItem($"Fast Local Operation Completed {_localOperationCounter}");
        }

        private async void btnWebAPICall_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://localhost:7133/TestLongOperation");

            string result = await httpResponseMessage.Content.ReadAsStringAsync();

            _webAPIOperationCounter++;

            AddListItem($"{result} {_webAPIOperationCounter}");
        }
    }
}
