using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using static System.Net.Mime.MediaTypeNames;
using Windows.Media.SpeechSynthesis;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpPlayProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //ApplicationView.PreferredLaunchViewSize = new Size(1000, 100);
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void btnHelloWorld_Click(object sender, RoutedEventArgs e)
        {
            string speechText = "Fourscore and seven years ago...";

            speakText(speechText);

            //Frame.Navigate(typeof(NewPage));
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewPage));
        }

        private async void speakText(string speechText)
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new SpeechSynthesizer();
            SpeechSynthesisStream stream = null;

            using (synth)
            {
                VoiceInformation voiceInfo =
                    (
                        from voice in SpeechSynthesizer.AllVoices
                        where voice.Gender == VoiceGender.Female
                        select voice
                    ).FirstOrDefault() ?? SpeechSynthesizer.DefaultVoice;

                synth.Voice = voiceInfo;
                stream = await synth.SynthesizeTextToStreamAsync(speechText);
            }

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}
