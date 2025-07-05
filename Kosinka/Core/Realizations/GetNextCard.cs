using Cosinka.Viewmodel.Realizations;

using Kosinka.Attributes;
using Kosinka.Core.Interfaces;
using Kosinka.Core.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Core.Realizations
{
    [UseInApp(KindOfRegistration.Transient)]
    public class GetNextCard : IGetNextCard
    {
        protected int i = -1;

        public Card? NextCard(IList<Card> cards)
        {
            if (cards.Count > 0)
                i = (i + 1) % cards.Count;
            else
                return null;
            cards[i].IsOpen = true;
            return cards[i];
        }
    }
}
