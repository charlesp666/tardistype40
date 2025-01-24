/***************************************************************************************************
* CustomDialog: DisplayInstructions
* 
* Custom Dialog that displays the instructions for playing the game.
* 
* @Copyright (c) 2025 Charles J. Pilgrim
* All Rights Reserved.
*/

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
