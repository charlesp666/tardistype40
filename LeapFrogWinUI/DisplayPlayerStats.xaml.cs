/***************************************************************************************************
* CustomDialog: DisplayPlayerStats
* 
* CustomDialog that displays the players information and game statistics.
* 
* @Copyright (c) 2025 Charles J. Pilgrim
* All Rights Reserved.
*/

using System;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LeapFrogWinUI
{
    public sealed partial class DisplayPlayerStats : ContentDialog
    {
        public DisplayPlayerStats(Player myPlayer)
        {
            this.InitializeComponent();

            int gamesPlayed = myPlayer.getGamesPlayed();
            int totalMoves = myPlayer.getCountMoves();
            TimeSpan ttlTimePlayed = myPlayer.getTimePlayed();

            tbGamesPlayed.Text = gamesPlayed.ToString();
            tbTotalScore.Text = totalMoves.ToString();
            tbTotalTime.Text = ttlTimePlayed.ToString(@"hh\:mm\:ss");
        }
    }
}
