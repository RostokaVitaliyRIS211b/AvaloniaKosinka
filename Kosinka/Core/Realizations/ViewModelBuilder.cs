using Cosinka.Viewmodel.Realizations;

using Kosinka.Attributes;
using Kosinka.Core.Interfaces;
using Kosinka.Core.Model;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Core.Realizations
{
    [UseInApp(KindOfRegistration.Transient)]
    public class ViewModelBuilder : IViewModelBuilder
    {
        private readonly Random random = new();
        public IEnumerable<ObservableCollection<Card>> GetDecks()
        {
            ObservableCollection<Card> deck = [];

            for (int i = 0; i<4; ++i)
            {
                for (int j = 1; j<14; ++j)
                {
                    deck.Add(new Card((CardRank)j, (CardSuit)i));
                }
            }

            List<ObservableCollection<Card>> cards = [];

            for (int i = 1; i<8; ++i)
            {
                ObservableCollection<Card> Somedeck = [];
                for (int j = 0; j<i; ++j)
                {
                    int random3 = random.Next(0, deck.Count-1);
                    deck[random3].Zindex = j;
                    Somedeck.Add(deck[random3]);
                    deck.RemoveAt(random3);
                }
                Somedeck[Somedeck.Count - 1].IsOpen = true;
                cards.Add(Somedeck);
            }
            Card[] cards1 = [.. deck];
            random.Shuffle(cards1);
            deck = new(cards1);
            cards.Add(deck);
            return cards;
        }
    }
}
