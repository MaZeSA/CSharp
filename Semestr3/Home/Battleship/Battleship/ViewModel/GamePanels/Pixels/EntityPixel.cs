using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels.Pixels
{
    public class EntityPixel : BaseVisualElement
    {
        public EntityPixel(GPanelView gPanelView, int row, int colum) : base(gPanelView)
        {
            VisulBoodies = new List<IBoody>();

            Row = row;
            Column = colum;
            BorderBrush = Brushes.Black;
            BackgroundBrush = new SolidColorBrush(Color.FromArgb(210, 127, 224, 230));
            BorderThickness = new Thickness(1, 1, 0, 0);
            VisulBoodies.Add(new Pixel(gPanelView, this, 0) { BackgroundBrush = this.BackgroundBrush }); 
        }

        public override bool? Shot(int row, int column, bool mis)
        {
            if (row != Row || column != Column) return null;
            if (mis)
            {
                VisulBoodies[0].ShotShow = true;
            }
            else
            {
                ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Battleship;component/Resources/vzriv.png", UriKind.RelativeOrAbsolute));
            }
          
            return false;
        }

    }
}
