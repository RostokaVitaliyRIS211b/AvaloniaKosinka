using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kosinka.Core.Model;


namespace Kosinka.Helpers
{
    public static class CardHelp
    {
        public const double Height = 165;
        public const double Width = 110;

        public static bool IsCompatable(Card card1,Card card2)
        {
            bool rank = card2.Rank-card1.Rank==1;
            bool suit = false;
            if(card1.Suit==CardSuit.Heart || card1.Suit==CardSuit.Diamond)
            {
                suit=card2.Suit==CardSuit.Club||card2.Suit==CardSuit.Spade;
            }
            else
            {
                suit = card2.Suit==CardSuit.Heart || card2.Suit==CardSuit.Diamond;
            }
            return rank && suit;
        }

        public static bool IsAceCompatable(Card card1, Card card2)
        {
            bool rank = card1.Rank-card2.Rank==1;
            bool suit = card1.Suit==card2.Suit;
            return rank && suit;
        }
    }
}
