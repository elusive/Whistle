namespace Elusive.Whistle.Core.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Class to represent a single hand of cards in a multiplayer card game.
    /// </summary>
    public class Hand
    {
        List<Card> CardsInHand { get; set; }
        List<Card> CardsPlayed { get; set; }

        void Sort()
        {
        }
    }
}
