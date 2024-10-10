﻿/***************************************************************************************************
 * Class: Cards
 * 
 * Defines the Data and Methods for a Deck of Cards by defining the partial
 * or sub-class of "Card" then extends this to a deck of cards. 
 * 
 * @Copyright (c) 2024 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeapFrogWPF
{
    /***************************************************************************************************
     * Cards Class Definition
     * Consists of two partial classes "Card" and "Deck"
     */
    internal class Cards
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Set Folder Locations for Card Images as static constants
        private static String folderGameImages = "/Assets/GameImages/";
        private static String folderCardFaces = "/Assets/CardImages/";

        /***********************************************************************************************
           * Partial Class Card:
           * Defines the Attributes and Methods of a Single Playing Card.
           */
        public partial class Card : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            /* Define arrays storing to possible values for rank and suit; make public to be viewable by
             * anyone wanting to build a card or deck of cards.
             ***** Q: how to handle (should?) jokers?
             */
            public static String[] possibleRanks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            public static char[] possibleSuits = { 'S', 'H', 'C', 'D' };     //Possible Values for Suits

            private string defaultCardBack = folderGameImages + "defaultBack.gif";

            private string strCardFace;

            //Declare Card Public Attributes to be used for displaying Cards in Game
            public string cardRank                       //Contains the Rank (Ace through King) of card
            {
                get;
                set;
            }

            public string cardSuit        //Contains the Suit (Spades, Hearts, Clubs, Diamonds) of Card
            {
                get;
                set;
            }

            public string cardFace                //Contains "Local" (Solution) Path to Card Face Image
            {
                get
                {
                    return strCardFace;
                }
                set
                {
                    if (value != this.strCardFace)
                    {
                        this.strCardFace = value;
                        NotifyPropertyChanged("cardFace");
                    }
                }
            }

            public string cardBack                 //Contains "Local" (Solution) Path to Card Back Image
            {
                get;
                set;
            }

            /*******************************************************************************************
             * Constructor: Card (Default)
             * Creates a new Card object and assigns the Rank and Suit passed as parameters,
             * and sets the card face to the default image.
             */
            public Card()
            {
                this.cardRank = null;                                    //initialize Card's Rank to null
                this.cardSuit = null;                                    //initialize Card's suit to null
                this.cardFace = null;                           //Initialize Card Face Image path to null
                this.cardBack = null;                           //Initialize Card Back Image path to null
            }

            /*******************************************************************************************
             * Constructor: Card
             * Creates a new Card object and assigns the Rank and Suit passed as parameters. Pulls
             * the Card Face from the local resources based on rank and suit.
             */
            public Card(string newRank, string newSuit)
            {
                String iconFile = newRank.ToString();                //Initialize the Filename with Rank

                //Get Card Rank for first character of filename
                try
                {
                    int aResult = int.Parse(iconFile);
                }
                catch
                {
                    //No Catch action necessary
                }

                //Build Path to Card Face File
                iconFile += (newSuit.ToString()).ToLower();                       //Add Suit to Filename
                iconFile += ".gif";                                          //Add extension to Filename

                string aCardFace = folderCardFaces + iconFile;           //Add folder file is located in

                setCard(newRank, newSuit, aCardFace);
            }

            /*******************************************************************************************
             * Constructor: Card
             * Creates a new Card object and assigns the Rank, Suit and Face Image passed as parameters.
             * the Card Face from the local Assembly resources based on rank and suit.
             * Used to Produce the Card object to mark the Playable and Non-Playable spaces.
             */
            public Card(string newRank, string newSuit, string cardImage)
            {
                setCard(newRank, newSuit, cardImage);
            }

            /*******************************************************************************************
             * Method (override) equals
             * Compares the card passed as parameter to the card that invoked the method.
             * Returns true if the cards have the same suit and rank; otherwise, returns
             * false.
             */
            public bool equals(Card someCard)
            {
                bool retVal = false;                      //Set Default return value to false

                if ((this.getRank().Equals(someCard.getRank())) && (this.getSuit() == someCard.getSuit()))
                {                                                   // If suit and rank match
                    retVal = true;                                 //Set return value to true
                }
                return retVal;
            }

            /*******************************************************************************************
             * Method: findRank
             * Returns the Position in the possibleRanks array where the Rank is Found
             */
            public int findRank(String aRank)
            {
                int retVal = -1;

                for (int aPos = 0; aPos < possibleRanks.Length; aPos++)
                    if (possibleRanks[aPos].Equals(aRank))
                    {
                        retVal = aPos;
                        break;
                    }

                return retVal;
            }

            /*******************************************************************************************
             * Method: getCardFace
             * Returns the Image on the Face of the Current Card
             */
            public string getCardFace()
            {
                return (this.cardFace);
            }

            /*******************************************************************************************
              * Method: getRank
              * Returns the rank of the current card object
              */
            public string getRank()
            {
                return (this.cardRank);
            }


            /*******************************************************************************************
             * Method: getRank
             * Parses the Rank from the CardValue passed.
             */
            public String getRank(String aCardValue)
            {
                int cardLength = aCardValue.Length;                     //Store Length of Card Value
                int rankLength = cardLength - 1;          //Determine Length of String for Card Rank

                return (aCardValue.Substring(0, rankLength));                   //Return Card's Rank
            }

            /*******************************************************************************************
             * Method: getSuit
             * Returns the suit of the current card object
             */
            public string getSuit()
            {
                return (this.cardSuit);
            }

            /*******************************************************************************************
             * Method: getSuit
             * Parses the Rank from the Card Value passed.
             */
            public char getSuit(String aCardValue)
            {
                int suitPosition = aCardValue.Length - 1;     //Determine Position in String of Suit

                return Convert.ToChar(aCardValue.Substring(suitPosition, 1));   //Return Card's Suit
            }

            /*******************************************************************************************
             * Method: setCard
             * Sets the Rank, Suit, Face and Back images of the Card.
             */
            public void setCard(string aRank, string aSuit, string aCardFace)
            {
                this.cardRank = aRank;
                this.cardSuit = aSuit;
                this.cardFace = aCardFace;
                this.cardBack = defaultCardBack;
            }

            /*******************************************************************************************
             * Method: setCardFace
             * Associates the image passed as a parameter as the face of the current card object.
             */
            private void setCardFace(string anImage)
            {
                this.cardFace = anImage;
            }

            /*******************************************************************************************
             * Method: setRank
             * Verifies the Rank passed as parameter is one of the accepted values, then
             * sets the Rank of the current Card object.
             */
            private void setRank(string newRank)
            {
                string aRank = null;

                for (int i = 0; i < possibleRanks.Length; i++)
                {
                    if (newRank.Equals(possibleRanks[i]))
                    {
                        aRank = newRank;
                    }
                } //Verify newRank is of possible values

                if (!(aRank is null))                           //If newRank is an acceptable value
                {
                    this.cardRank = aRank;                                   //Set the object's rank
                }
            }

            /*******************************************************************************************
             * Method: setSuit
             * Verifies the Suit passed as parameter is one of the accepted values, then
             * sets the Suit of the current Card object.
             */
            private void setSuit(string newSuit)
            {
                string aSuit = null;

                for (int i = 0; i < possibleSuits.Length; i++)    //Verify newSuit is of possible values
                {
                    if (newSuit == possibleSuits[i].ToString())
                        aSuit = newSuit;
                }

                if (aSuit != null)                                //If newSuit is acceptable value...
                    this.cardSuit = aSuit;                                   //Set the object's suit
            }
        }

        /***********************************************************************************************
         * Class Cards:
         * Defines the Attributes and Methods of a Single Playing Card.
         */

        /*******************************************************************************************
         * Class Variables and Constants
         */
        private static Random aRandom = new Random();       //Parameter for Random Number Generation

        public BindingList<Card> deckCards = new BindingList<Card>();       //Declare List to store Deck of Cards for game play

        //Load the Default Card Back Image
        private string cardBack = "/Assets/GameImages/defaultBack.jpg";

        //Set CardFace to Default "NotPlayable" Image
        private string bmpNotPlayable = folderGameImages + "NotPlayable.jpg";
        private string bmpPlayable = folderGameImages + "Playable.jpg";

        private int countShuffle = 5000;                 //Number of times to swap cards for Shuffle

        private Card PlayingCard;

        public Card playingCard
        {
            get { return PlayingCard; }
            set
            {
                if (value != this.PlayingCard)
                {
                    this.PlayingCard = value;
                    NotifyPropertyChanged("playingCard");
                }
            }
        }

        /*******************************************************************************************
         * Constructor: Cards()
         * Builds a deck of cards by creating an array of Card objects
         */
        public Cards()
        {
            //Configure the Binding list
            deckCards.AllowNew = true;
            deckCards.AllowEdit = true;

            initializeDeck();
        }

        /*******************************************************************************************
         * Method: calcArrayPosition
         * Computes the array position based on the Rank (Column) and Suit (row).
         */
        public int calcArrayPosition(int aRow, int aCol)
        {
            return (aRow * Cards.Card.possibleRanks.Length) + aCol;
        }

        /*******************************************************************************************
         * Method: cutDeck
         * Process randomly selects a card in the Deck, then rotates the cards from bottom
         * to top until that card is reached thereby effective a "cut" of the deck of cards.
         */
        public void cutDeck()
        {
            Card aTemp = new Card();
            int numberOfCards = this.deckCards.Count;     //Difference between number and array
            int cardToCutAt = 0;

            cardToCutAt = (int)aRandom.Next(0, numberOfCards);

            for (int aCard = 0; aCard < cardToCutAt; aCard++)
            {
                aTemp.setCard(this.deckCards[numberOfCards - 1].getRank(), this.deckCards[numberOfCards - 1].getSuit(), this.deckCards[numberOfCards - 1].getCardFace());

                for (int aCut = numberOfCards - 1; aCut > 0; aCut--)
                {
                    this.deckCards[aCut].setCard(this.deckCards[aCut - 1].getRank(), this.deckCards[aCut - 1].getSuit(), this.deckCards[aCut - 1].getCardFace());
                }
                this.deckCards[0].setCard(aTemp.getRank(), aTemp.getSuit(), aTemp.getCardFace());
            }
        }

        /*******************************************************************************************
         * Method: getCard
         * Returns the Card object found at the specified position in the Deck.
         */
        public Card getCard(int aCardPosition)
        {
            return deckCards[aCardPosition];
        }

        /*******************************************************************************************
         * Method: getCardBack
         * Returns the Image on the Back of the Current Card
         */
        public string getCardBack()
        {
            return this.cardBack;
        }

        /*******************************************************************************************
         * Method: getCardFace
         * Returns the Image on the Face of the Current Card
         */
        public string getCardFace(int aCardPosition)
        {
            return deckCards[aCardPosition].getCardFace();
        }

        /*******************************************************************************************
         * Method: getCardFaceNotPlayable
         * Returns the Image on the Face of a Non-Playable Card
         */
        public string getCardFaceNotPlayable()
        {
            return bmpNotPlayable;
        }

        /*******************************************************************************************
         * Method: getCardFacePlayable
         * Returns the Image on the Face of a Non-Playable Card
         */
        public string getCardFacePlayable()
        {
            return bmpPlayable;
        }

        /*******************************************************************************************
         * Method: getNextCardAscending
         * Returns the Card Value of the Next Ascending Card
         */
        public String getNextCardAscending(String aCardFace)
        {
            Card aCard = new Card();                  //Dummy Card Object to Access Card Methods

            String cardRank = getRank(aCardFace);                 //Get the Rank of Current Card
            String cardSuit = getSuit(aCardFace);                 //Get the Suit of Current Card

            int nextPosition = aCard.findRank(cardRank);    //Get Array position of Current Rank
            nextPosition++;                           //Increment Position to Next Rank in Array
            if (nextPosition < Card.possibleRanks.Length - 1)
            {
                cardRank = Card.possibleRanks.ElementAt(nextPosition);
            }
            else
            {
                cardRank = null;
                cardSuit = null;
            }

            return (cardRank + cardSuit);
        }

        /*******************************************************************************************
         * Method: getNextCardDescending
         * Returns the Card Value of the Next Descending Card
         */
        public String getNextCardDescending(String aCardFace)
        {
            Card aCard = new Card();                  //Dummy Card Object to Access Card Methods

            String cardRank = getRank(aCardFace);                 //Get the Rank of Current Card
            String cardSuit = getSuit(aCardFace);                 //Get the Suit of Current Card

            int nextPosition = aCard.findRank(cardRank);    //Get Array position of Current Rank
            nextPosition--;                           //Increment Position to Next Rank in Array
            if (nextPosition >= 0)
            {
                cardRank = Card.possibleRanks.ElementAt(nextPosition);
            }
            else //
            {
                cardRank = null;
                cardSuit = null;
            }

            return (cardRank + cardSuit);
        }

        /*******************************************************************************************
         * Method: getRank
         * Parses the Rank from the CardValue passed.
         */
        public String getRank(String aCardValue)
        {
            int cardLength = aCardValue.Length;                     //Store Length of Card Value
            int rankLength = cardLength - 1;          //Determine Length of String for Card Rank

            return (aCardValue.Substring(0, rankLength));                   //Return Card's Rank
        }

        /*******************************************************************************************
         * Method: getSuit
         * Parses the Rank from the Card Value passed.
         */
        public String getSuit(String aCardValue)
        {
            int suitPosition = aCardValue.Length - 1;     //Determine Position in String of Suit

            return (aCardValue.Substring(suitPosition, 1));             //Return the Card's Suit
        }

        /*******************************************************************************************
         * Method: initializeDeck
         * Creates (or recreates) an original deck of cards in sorted order: A - K and
         * Spades - Diamonds.
         */
        public void initializeDeck()
        {
            //Card playingCard;                   //Local working storage to make coding more readable

            for (int aSuit = 0; aSuit < Card.possibleSuits.Length; aSuit++)
            {
                for (int aRank = 0; aRank < Card.possibleRanks.Length; aRank++)
                {
                    playingCard = new Card(Card.possibleRanks[aRank], Card.possibleSuits[aSuit].ToString());
                    deckCards.Add(playingCard);
                    deckCards.ResetItem(calcArrayPosition(aRank, aSuit));
                }
            }
        }

        /*******************************************************************************************
         * Method: sameRank
         * Returns "True" if the two cards passed as parameters have the Same Rank.
         */
        public bool sameRank(Card thisCard, Card testCard)
        {
            return (thisCard.getRank() == testCard.getRank());
        }

        /*******************************************************************************************
         * Method: sameSuit
         * Returns "True" if the two cards passed as parameters are in the same Suit.
         */
        public bool sameSuit(Card thisCard, Card testCard)
        {
            return (thisCard.getSuit() == testCard.getSuit());
        }

        /*******************************************************************************************
         * Method: shuffleDeck
         * Shuffles deck by randomly moving cards between entries in the deck array.
         */
        public void shuffleDeck()
        {
            Card aTemp = new Card();
            int numberOfCards = this.deckCards.Count;
            int firstCard = 0;
            int secondCard = 0;

            //Find the two cards to swap
            for (int aCount = 0; aCount <= countShuffle; aCount++)
            {
                firstCard = (int)aRandom.Next(0, numberOfCards);
                do
                {
                    secondCard = (int)aRandom.Next(0, numberOfCards);
                } while (firstCard == secondCard);

                aTemp = deckCards[firstCard];
                deckCards[firstCard] = deckCards[secondCard];
                deckCards[secondCard] = aTemp;

                deckCards.ResetItem(firstCard);
                deckCards.ResetItem(secondCard);
            }
        }
    }
}
}