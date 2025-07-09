using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Ace = 1,
    }

    public enum CardSuit
    {
        Heart = 0,
        Diamond,
        Club,
        Spade
    }

    public class Card : INotifyPropertyChanged,IComparable
    {
        public required CardRank Rank { get; init; }
        public required CardSuit Suit { get; init; }

        private int _z = 0;
        public int ZIndex { get => _z; set { _z = value; OnPropertyChanged(nameof(ZIndex)); } }

        private bool _isOpen = false;
        public bool IsOpen { get => _isOpen; set { _isOpen = value; OnPropertyChanged(nameof(IsOpen)); } }

        public Card()
        {

        }

        [SetsRequiredMembers]
        public Card(CardRank cardRank, CardSuit cardSuit)
        {
            Rank = cardRank;
            Suit = cardSuit;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(object? obj)
        {
            if(obj is Card card)
            {
                if(card.Rank == Rank && card.Suit==Suit)
                {
                    return 0;
                }
                else if (card.Rank > Rank)
                {
                    return -1;
                }
                else if(card.Rank < Rank)
                {
                    return 1;
                }
                else if(card.Suit > Suit)
                {
                    return -1;
                }
                else if(card.Suit < Suit)
                {
                    return 1;
                }
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
