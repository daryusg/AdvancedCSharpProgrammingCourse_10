using EmployeeComponent;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EmployeeUWPApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window //Part 11 - Events - User Actions & UWP
    {
        ObservableCollection<EmployeeViewModel> _employeesOC = null!;
        //chatgpt public XamlUICommand ChangeFirstNameCommand = null;
        public XamlUICommand ChangeFirstNameCommand { get; set; } = null!;
        
        private Windows.Media.Playback.MediaPlayer _mediaPlayer = new(); //chatgpt

        public MainWindow()
        {
            InitializeComponent();


            Employees employees = new Employees();

            _employeesOC = employees.GetEmployees();

            EmployeesList.ItemsSource = _employeesOC;

            ChangeFirstNameCommand = new XamlUICommand();
            ChangeFirstNameCommand.ExecuteRequested += ChangeFirstNameCommand_ExecuteRequested;

            btnThankYou.Click += BtnThankYou_Click;
        }

        private async void BtnThankYou_Click(object sender, RoutedEventArgs e)
        {
            await SpeakAsync("Thank you, and take care.");
        }

        private async void ChangeFirstNameCommand_ExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            ListView lv = (ListView)args.Parameter;
            if (lv.SelectedIndex != -1)
            {
                await SpeakAsync($"Changing first name, from {_employeesOC[lv.SelectedIndex].FirstName}, to, {txtFirstName.Text}");
                _employeesOC[lv.SelectedIndex].FirstName = txtFirstName.Text;
            }

        }
        //chatgpt MediaElement is no longer valid.
        //private async Task SpeakAsync(string text)
        //{

        //    MediaElement mediaElement = new MediaElement();
        //    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
        //    Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);
        //    mediaElement.SetSource(stream, stream.ContentType);

        //}
        private async Task SpeakAsync(string text)
        {
            //tailored from https://learn.microsoft.com/en-us/uwp/api/windows.media.speechsynthesis.voicegender?view=winrt-28000
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                Random random = new Random();
                VoiceGender[] genders = Enum.GetValues<VoiceGender>();
                VoiceGender gender = genders[Random.Shared.Next(genders.Length)];

                VoiceInformation voiceInfo =
                    (
                        from voice in SpeechSynthesizer.AllVoices
                        where voice.Gender == gender
                        select voice
                    ).FirstOrDefault() ?? SpeechSynthesizer.DefaultVoice;

                synthesizer.Voice = voiceInfo;

                var stream = await synthesizer.SynthesizeTextToStreamAsync(text);
                _mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromStream(stream, stream.ContentType);
                _mediaPlayer.Play();
            }
        }
    }
}
