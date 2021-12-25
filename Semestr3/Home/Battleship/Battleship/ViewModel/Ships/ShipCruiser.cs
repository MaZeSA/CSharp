using Battleship.ViewModel.GamePanels.Pixels;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Battleship.ViewModel.Ships
{
    public class ShipCruiser : Ship
    {
        public ShipCruiser(GPanelView gPanelView/*, int row, int column*/) : base(gPanelView)
        {
            Length = 4;
            Row = 1;
            Column = 1;
            ColumnSpan = Length;
            BitmapUri = new Uri("/Battleship;component/Resources/Cruiser.png", UriKind.RelativeOrAbsolute);
            ImageSource = new BitmapImage(BitmapUri);
            BorderThickness = new System.Windows.Thickness(1);
            BackgroundBrush = new SolidColorBrush(Color.FromArgb(190, 176, 224, 230));

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new Pixel(gPanelView, this, i) { BackgroundBrush = this.BackgroundBrush});
            }
        }
    }
}
