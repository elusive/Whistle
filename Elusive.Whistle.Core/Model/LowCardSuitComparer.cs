namespace Elusive.Whistle.Core.Model
{
using System.Collections.Generic;

    /// <summary>
    /// Comparer for downtown sorting. (Ace Low)
    /// </summary>
    public class LowCardSuitComparer : IComparer<Card>
    {
        #region IComparer<Card> Members

        public int Compare(Card x, Card y)
        {
            if (x.Suit == y.Suit)
            {
                return x.CompareTo(y);
            }

            if (x.Suit < y.Suit)
                return -1;
            else
                return 1;
        }

        #endregion
    }
}
