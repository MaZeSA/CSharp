﻿using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.GamePanels.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Battleship.ViewModel.Ships
{
    public class ShipCorvette: Ship
    {
        public ShipCorvette(GameModel gameModel, int row, int column) : base(gameModel)
        {
            Length = 1;
            Row = row;
            Column = column;
            ColumnSpan = Length;
            //BitmapUri = new Uri("pack://application:,,,/Resources/Corvette.png");
            BitmapUri = new Uri("/Battleship;component/Resources/Corvette.png", UriKind.RelativeOrAbsolute);
            ImageSource = new BitmapImage(BitmapUri);
            BorderThickness = new System.Windows.Thickness(1);
            BackgroundBrush = new SolidColorBrush(Color.FromArgb(190, 176, 224, 230));

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new Pixel(gameModel, this, i) { BackgroundBrush = this.BackgroundBrush });
            }
        }
    }
}