using Avalonia.Media;
using Avalonia.Media.Imaging;
using Kosinka.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Core.Interfaces
{
    public interface IGetImageOfCard
    {
        public IImage GetImage(Card card);
    }
}
