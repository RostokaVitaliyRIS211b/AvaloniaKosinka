﻿using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Cosinka.Viewmodel.Interfaces;

using Kosinka.Attributes;
using Kosinka.Core.Interfaces;
using Kosinka.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Cosinka.Viewmodel.Realizations
{
    [UseInApp(KindOfRegistration.Singleton)]
    public class GetImageOfCard : IGetImageOfCard
    {
        public Bitmap CardsMapImage;
        public GetImageOfCard()
        {
            string currentLocation = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(currentLocation, "..", "..", "..", "..", "Kosinka","Assets", "cards.png");
            CardsMapImage = new Bitmap(imagePath);
        }

        public IImage GetImage(Card card)
        {
            PixelRect int32Rect = new(12 + 122 * ((int)card.Rank - 1), 12 + 180 * (int)card.Suit, 110, 165);
            CroppedBitmap bitmap = new(CardsMapImage, int32Rect);
            return bitmap;
        }
    }
}
