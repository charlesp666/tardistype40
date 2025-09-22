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
        private int currentMoves = 0;                            //Store the Current Value for Moves
        private TimeSpan totalTimePlayed;                              //Store the Total Time Played

        //"Public" Xaml access for Playable and Non-Playable Cards
        public Cards.Card cardPlayable;
        public Cards.Card cardNotPlayable;

        /*******************************************************************************************
         * Constructor: GameScore (Default)
         */
        public GameScore(Cards gameDeck, int theMoves, TimeSpan timePlayed)
        {
            //Create the "Playable" and "Not-Playable" Cards for Position Markers
            cardPlayable = new Cards.Card("p", "l", gameDeck.getCardFacePlayable());
            cardNotPlayable = new Cards.Card("n", "p", gameDeck.getCardFaceNotPlayable());

            currentMoves = theMoves;                          //Store the value for number of Moves
            totalTimePlayed.Add(timePlayed);                  //Store the vlue of Total Time Played

            gameScore = scoreGame(gameDeck);
        }

        /*******************************************************************************************
         * Function: getCurrentMoves
         * Return the Move Count
         */
        public int getCurrentMoves()
        {
            return currentMoves;
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
         * Function: getTimePlayed
         * Return the Time Played
         */
        public TimeSpan getTimePlayed()
        {
            return totalTimePlayed;
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
         * Method: isCardPositionMarker
         * Checks if the Card passed as parameter is either the "Playable" or "Not-Playable" 
         * position markers.
         */
        private bool isCardPositionMarker(Cards.Card testCard)
        {
            bool returnResult = false;

            if ((testCard.cardsMatch(cardNotPlayable)) || (testCard.cardsMatch(cardPlayable)))
            {
                returnResult = true;
            }

            return returnResult;  
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

            // Sum score for cards that are in correct sequence and correct position; Use the count
            // of Suits to process each row.
            for (int aSuit = 0; aSuit < Cards.Card.possibleSuits.Length; aSuit++)
            {
                countSequence = 0;                     //Ensure Sequence Count is reset for each row
                int currentRank = 0;        //Set initial column (Rank) position for the current row
                bool correctPosition = false;          //Initialize "Correct Position" flag to "Not"

                while (currentRank < 12)
                {
                    bool doCardsMatch = false;        //Flag indicating Cards are in proper sequence

                    //Compute the Play Position of the Current Card being checked
                    int playPosition = gameDeck.calcArrayPosition(aSuit, currentRank);

                    Cards.Card thisCard = gameDeck.deckCards[playPosition];              //This Card
                    if (!isCardPositionMarker(thisCard))
                    {
                        Cards.Card nextCardInSequence = gameDeck.findNextCardDescending(thisCard);

                        Cards.Card nextCard = gameDeck.deckCards[playPosition + 1];      //Next Card
                        if (!isCardPositionMarker(nextCard))
                        {
                            doCardsMatch = nextCard.cardsMatch(nextCardInSequence);
                        }
                    }

                    if (doCardsMatch)                                   //If next card is correct...
                    {
                        countSequence++;                            //Increment the Sequence Counter
                        if (isCorrectPosition(playPosition, gameDeck))   //If in correct position...
                        {
                            correctPosition = true;       //Set the "Correct Position" flag to "Yes"
                        }
                    }
                    else                   //The End of a sequence is found, process the sequence...
                    {
                        countSequence++;                      //Adjust Sequence Count for first card

                        if (countSequence == 12)                        //If the suit is complete...
                        {
                            completedSuits++;                //Increment the Completed Suits counter
                            thisGameScore += pointsForCompleteSuit;            //Add points to score
                        }

                        if (countSequence > 2)      //If at least 3 cards are in correct sequence...
                        {
                            thisGameScore += (countSequence * pointsForSequence);       //Add points

                            if (correctPosition)               //If cards are in correct position...
                            {
                                thisGameScore += (countSequence * pointsForPosition);   //Add points
                            }
                        }

                        countSequence = 0;                                  //Reset sequence Counter
                        correctPosition = false;                     //And the Correct Position Flag
                    }

                    currentRank++;                                                //Go the next card
                }
            }

            //Add bonus multiplier here

            if (completedSuits == 4)                                  //If all Suits are completed...
            {
                thisGameScore += gameWinningBonus;                          //Add Winning Game Bonus
            }

            return thisGameScore;
        }
    }
}
