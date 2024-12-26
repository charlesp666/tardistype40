using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LeapFrogWinUI
{
    public sealed partial class DisplayInstructions : ContentDialog
    {
        public DisplayInstructions(string gameInstructions)
        {
            this.InitializeComponent();

            tbPlayInstructions.Text = gameInstructions;
        }
    }
}
