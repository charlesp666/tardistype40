/***************************************************************************************************
* Object Class: GameScore
* 
* Class Used to compute the score for the game.
* 
* @Copyright (c) 2025 Charles J. Pilgrim
* All Rights Reserved.
*/

using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace LeapFrogWinUI
{
    internal class GameScore
    {
        // Define parameters for Scoring Games (Determining Player's Winnings)
        private int pointsForSequence = 1;             //Points to add for cards in correct sequence
        private int pointsForPosition = 2;             //Points to add for cards in correct position
        private int pointsForCompleteSuit = 10;                  //Points to add for a complete suit

        private int gameWinningBonus = 100;      //Bonus Amount for a All Cards Correctly Positioned

        private int gameScore = 0;                            //Accumulator for the Total Game Score

        //"Public" Xaml access for Playable and Non-Playable Cards
        public Cards.Card cardPlayable;
        public Cards.Card cardNotPlayable;

        /*******************************************************************************************
         * Constructor: GameScore (Default)
         */
        public GameScore(Cards gameDeck)
        {
            gameScore = scoreGame(gameDeck);
        }

        /*******************************************************************************************
         * Function: getGameScore
         * Return the Computed Game Score
         */
        public int getGameScore()
        { 
            return gameScore;
        }

        /*******************************************************************************************
         * Function: isCorrectPosition
         * Compares card position in row and determines if this is correctly placed. Returns "true"
         * if card is in correct position; otherwise returns false.
         */
        private bool isCorrectPosition(int cardPosition, Cards gameDeck)
        {
            Cards.Card thisCard = gameDeck.deckCards[cardPosition];          //Get Card being tested
            bool placedCorrectly = false;                                 //Set default return value

            if (!(thisCard.Equals(cardNotPlayable)))
            {
                String thisRank = thisCard.getRank();                        //Get Current Card Rank
                int correctPosition = thisCard.findRank(thisRank);  //Get Position in Possible Ranks
                correctPosition = Math.Abs(correctPosition - 12);         //Adjust for Reverse Order

                int thisPosition = (cardPosition % 13);           //Get Current Card Column Position

                placedCorrectly = (thisPosition == correctPosition);       //Compute Correct Placing
            }

            return placedCorrectly;
        }

        /*******************************************************************************************
         * Method: scoreGame
         * Adds up the score of the current game; scoring as follows:
         * 
         * 2 points for each card in correct sequence (by rank and suit)
         * 5 points for each card in correct position (column by rank)
         * 10 points for completion of a suit (King through 2 of same suit on same row)
         */
        private int scoreGame(Cards gameDeck)
        {
            int thisGameScore = 0;              //Local variable to accumulate score of current game
            int completedSuits = 0;                          //Count of the Suits that are completed
            int countSequence = 0;                   //Count the number of cards in correct sequence

            // Sum score for cards that are in correct sequence and correct position
            for (int aSuit = 0; aSuit < Cards.Card.possibleSuits.Length; aSuit++)
            {
                countSequence = 0;                     //Ensure Sequence Count is reset for each row
                int currentRank = 0;       //Set initial column or Rank position for the current row
                bool correctPosition = false;          //Initialize "Correct Position" flag to "Not"

                while (currentRank < 12)
                {
                    //Compute the Play Position of the Current Card being checked
                    int playPosition = gameDeck.calcArrayPosition(aSuit, currentRank);

                    Cards.Card thisCard = gameDeck.deckCards[playPosition];              //This Card
                    if (!thisCard.cardsMatch(cardNotPlayable))
                    {
                        Cards.Card nextCard = gameDeck.deckCards[playPosition + 1];          //Next Card

                        Cards.Card nextCardInSequence = gameDeck.findNextCardDescending(thisCard);

                        if (nextCard.cardsMatch(nextCardInSequence))        //If next card is correct...
                        {
                            countSequence++;                            //Increment the Sequence Counter
                            if (isCorrectPosition(playPosition, gameDeck)) //If Current Card is in correct position...
                            {
                                correctPosition = true;       //Set the "Correct Position" flag to "Yes"
                            }
                        }
                        else               //Card is in Current position, begin checking for sequence...
                        {
                            countSequence++;                      //Adjust Sequence Count for first card

                            if (countSequence == 12)                         //If the suit is complete...
                            {
                                completedSuits++;                //Increment the Completed Suits counter
                                thisGameScore += pointsForCompleteSuit; //Add Completed Suits points to score
                            }

                            if (countSequence > 2)       //If at least 3 cards are in correct sequence...
                            {
                                thisGameScore += (countSequence * pointsForSequence); //Add points to score

                                if (correctPosition)                //If cards are in correct position...
                                {
                                    thisGameScore += (countSequence * pointsForPosition); //Add points to score
                                }
                            }

                            countSequence = 0;                                  //Reset sequence Counter
                            correctPosition = false;                     //And the Correct Position Flag
                        }
                    }

                    currentRank++;                                                //Go the next card
                }
            }

            if (completedSuits == 4)                                  //If all Suits are completed...
            {
                thisGameScore += gameWinningBonus;                          //Add Winning Game Bonus
            }

            return thisGameScore;
        }
    }
}
