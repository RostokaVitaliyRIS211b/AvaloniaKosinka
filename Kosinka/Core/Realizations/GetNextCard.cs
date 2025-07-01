using Kosinka.Core.Interfaces;
using Kosinka.Core.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Core.Realizations
{
    public class GetNextCard : IGetNextCard
    {
        protected int i = 0;

        public Card? NextCard(IList<Card> cards)
        {
            if (cards.Count > 0)
                i = (i + 1) % cards.Count;
            else
                return null;
            return cards[i];
        }
    }
}
