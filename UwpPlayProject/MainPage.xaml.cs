using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
//using static MainPageViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpPlayProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        public MainPaigeViewModel viewModel;
        private int maxIterations = 10;

        public MainPage()
        {
            this.InitializeComponent();

            viewModel = new MainPaigeViewModel();

            //viewModel.MyValue = "First Message...";
            //delay(5000);

            //viewModel.MyValue = "Second Message...";
            //delay(5000);

            //viewModel.MyValue = "Final Message...";
            //delay(5000);

            Task.Run(async () => {Random random = new Random();
                                  for (int i = 0; i < maxIterations; i++)
                                     {
                                         await Task.Delay(1000);
                                         int newValue = random.Next(-40, 40);
                                         try
                                         {
                                             await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                             () =>
                                             {
                                                 viewModel.ThisIteration = "Iteration: " + i.ToString();
                                                 viewModel.MyValue = newValue.ToString();
                                             });
                                         }
                                         catch (Exception ex)
                                         {
                                             string s = ex.ToString();
                                         }
                                     }
                                 }
            );

            viewModel.MyValue = "Final Message...";
            delay(5000);
        }

        /*******************************************************************************************
         * Method: delay
         * Pause Processing for specified number of milliseconds.
         */
        private void delay(int milliSecondsToPauseFor)
        {
            System.DateTime startInstant = System.DateTime.Now;
            System.DateTime thisInstant = startInstant;
            System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, milliSecondsToPauseFor);
            System.DateTime finalInstant = thisInstant.Add(duration);

            while (finalInstant >= thisInstant)
            {
                thisInstant = System.DateTime.Now;
            }
        }
    }

    public class MainPaigeViewModel : NotifyBase
    {
    //public MainPaigeViewModel()
    //{
    //    Task.Run(async () =>
    //    {
    //        Random random = new Random();
    //        while (true)
    //        {
    //            await Task.Delay(1000);
    //            int newValue = random.Next(-40, 40);
    //            try
    //            {
    //                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
    //                () => {
    //                    MyValue = newValue.ToString();
    //                });

    //            }
    //            catch (Exception ex)
    //            {
    //                string s = ex.ToString();
    //            }
    //        }
    //    });
    //}

        //Properties
        private string _MyValue;
        public string MyValue
        {
            get { return _MyValue; }
            set
            {
                _MyValue = value;
                OnPropertyChanged();
            }
        }

        private string _thisIteration;
        public string ThisIteration
        {
            get { return _thisIteration; }
            set
            {
                _thisIteration = value;
                OnPropertyChanged();
            }
        }
    }

    public abstract class NotifyBase : INotifyPropertyChanged
   {
       public event PropertyChangedEventHandler PropertyChanged = delegate { };
   
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           if (PropertyChanged != null)
           {
               PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
           }
       }
   }
}
