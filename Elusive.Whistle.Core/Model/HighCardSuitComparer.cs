namespace Elusive.Whistle.Core.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Comparer for uptown sorting (Ace High).
    /// </summary>
    public class HighCardSuitComparer : IComparer<Card>
    {
        #region IComparer<Card> Members

        public int Compare(Card x, Card y)
        {
            if (x.Suit != y.Suit)
            {
                if (x.Suit < y.Suit)
                    return -1;
                else return 1;
            }
            else
            {
                return y.CompareTo(x);
            }
        }

        #endregion
    }
}
