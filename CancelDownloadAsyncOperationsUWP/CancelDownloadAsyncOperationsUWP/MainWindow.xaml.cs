using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


//Part 20 - Async / Await Task - Cancelling Asynchronous Operations
//based on: https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/cancel-an-async-task-or-a-list-of-tasks
//https://github.com/GavinLonDigital/CancelDownloadAsyncOperationsUWP/blob/master/CancelDownloadAsyncOperationsUWP/MainPage.xaml.cs
//reads:
//https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/start-multiple-async-tasks-and-process-them-as-they-complete
//https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/using-async-for-file-access

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CancelDownloadAsyncOperationsUWP
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        //readonly CancellationTokenSource s_cts = new CancellationTokenSource();
        CancellationTokenSource s_cts = new CancellationTokenSource();

        readonly HttpClient s_client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };

        readonly IEnumerable<string> s_urlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/azure/devops",
            "https://docs.microsoft.com/dotnet",
            "https://docs.microsoft.com/dynamics365",
            "https://docs.microsoft.com/education",
            "https://docs.microsoft.com/enterprise-mobility-security",
            "https://docs.microsoft.com/gaming",
            "https://docs.microsoft.com/graph",
            "https://docs.microsoft.com/microsoft-365",
            "https://docs.microsoft.com/office",
            "https://docs.microsoft.com/powershell",
            "https://docs.microsoft.com/sql",
            "https://docs.microsoft.com/surface",
            "https://docs.microsoft.com/system-center",
            "https://docs.microsoft.com/visualstudio",
            "https://docs.microsoft.com/windows",
            "https://docs.microsoft.com/xamarin"
        };
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

        private void ClearListItems() //me
        {
            lvwOutput.Items.Clear();
        }

        private async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            int total = 0, index = 1;
            foreach (string url in s_urlList)
            {
                int contentLength = await ProcessUrlAsync(index++, url, s_client, s_cts.Token);
                total += contentLength;
            }

            stopwatch.Stop();

            AddListItem($"Total bytes returned:  {total:#,#}");
            //AddListItem($"Elapsed time:          {stopwatch.Elapsed}");
            TimeSpan ts = stopwatch.Elapsed; //me
            AddListItem($"Elapsed time:          {ts.Hours:D2}h {ts.Minutes:D2}m {ts.Seconds:D2}s {ts.Milliseconds:000}ms"); //me

        }
        //private async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
        private async Task<int> ProcessUrlAsync(int index, string url, HttpClient client, CancellationToken token)
        {
            HttpResponseMessage response = await client.GetAsync(url, token);
            //byte[] content = await response.Content.ReadAsByteArrayAsync();
            byte[] content = await response.Content.ReadAsByteArrayAsync(token);
            //AddListItem($"{url,-60} {content.Length,10:#,#}");
            AddListItem($"{index,3}. {url,-60} {content.Length,10:#,#}");

            return content.Length;
        }

        private async void btnDownloadContent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //me--->
                var s_cts_state = s_cts.TryReset();
                if (!s_cts_state)
                {
                    // Already cancelled or failed to reset; create new source
                    ClearListItems(); //me
                    s_cts.Dispose();
                    s_cts = new CancellationTokenSource();
                }
                //<---me
                //s_cts.CancelAfter(3500);
                if (Random.Shared.NextDouble() < 0.25) //cancel after 3.5 seconds 25% of the time
                {
                    s_cts.CancelAfter(3500);
                }
                await SumPageSizesAsync();
            }
            catch (TaskCanceledException)
            {
                AddListItem("Download Operations Cancelled");
            }

        }

        private void btnCancelDownload_Click(object sender, RoutedEventArgs e)
        {
            s_cts.Cancel();
        }
    }
}

