using Kosinka.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kosinka.Core.Interfaces
{
    public interface IViewModelBuilder
    {
        public IEnumerable<ObservableCollection<Card>> GetDecks();
    }
}
