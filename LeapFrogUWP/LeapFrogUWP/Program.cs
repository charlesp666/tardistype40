using System;
using System.Windows.Forms;

namespace LeapFrog
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Launch the Splash Screen
            LeapFrogSplashScreen introScreen = new LeapFrogSplashScreen();
            Application.Run();
        }
    }
}
