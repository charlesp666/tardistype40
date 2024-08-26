using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpPlayProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewPage : Page
    {
        private List<Car> cars;

        public NewPage()
        {
            this.InitializeComponent();
            cars = CarManager.GetCars();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var cars = (Car)e.ClickedItem;
            string speechText = "You Picked " + cars.Category.ToString() + " " + cars.Model.ToString();
            speakText(speechText);
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

    public class Car
    {
        public int CarId
        {
            get;
            set;
        }
        public string Category
        {
            get;
            set;
        }
        public string Model
        {
            get;
            set;
        }
        public string Image
        {
            get;
            set;
        }
    }

    public class CarManager
    {
        public static List<Car> GetCars()
        {
            var cars = new List<Car>();
            cars.Add(new Car
            {
                Category = "Honda",
                Model = "2014",
                Image = "Assets/GameImages/CardsIcon.png"
            });
            cars.Add(new Car
            {
                Category = "City",
                Model = "2008",
                Image = "Assets/GameImages/defaultBack.gif"
            });
            cars.Add(new Car
            {
                Category = "Ferrari",
                Model = "2010",
                Image = "Assets/GameImages/LeapFrog.jpg"
            });
            cars.Add(new Car
            {
                Category = "Toyota",
                Model = "2011",
                Image = "Assets/GameImages/NotPlayable.gif"
            });
            cars.Add(new Car
            {
                Category = "Mehran",
                Model = "2009",
                Image = "Assets/GameImages/Playable.gif"
            });
            return cars;
        }
    }
}
