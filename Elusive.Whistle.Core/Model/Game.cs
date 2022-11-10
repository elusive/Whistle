namespace Elusive.Whistle.Core.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Game class for game level settings and operations.
    /// </summary>
    public class Game
    {
        #region Properties

        private List<Card> cards = new List<Card>();
        public List<Card> Cards
        {
            get
            {
                return cards;
            }
        }

        private List<Deck> decks = new List<Deck>();
        public List<Deck> Decks
        {
            get
            {
                return decks;
            }
        }

        internal Random random = new Random();
        internal HighCardSuitComparer CardSuitComparer = new HighCardSuitComparer();

        #endregion
    }
}
