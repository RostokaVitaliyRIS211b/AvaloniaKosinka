using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Core.Model
{
    public enum CardRank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace=1,
    }

    public enum CardSuit
    {
        Heart = 0,
        Diamond,
        Club,
        Spade
    }

    public class Card
    {
        public required CardRank Rank { get; init; }
        public required CardSuit Suit { get; init; }

        public Card()
        {

        }

        [SetsRequiredMembers]
        public Card(CardRank cardRank, CardSuit cardSuit)
        {
            Rank = cardRank;
            Suit = cardSuit;
        }
    }
}
