using Battleship.Commands;
using Battleship.ViewModel.Pixels;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel
{
    public class ViewContext
    {
        public ObservableCollection<Pixel> Pixels { set; get; } = new ObservableCollection<Pixel>();
        public Pixel[][] PixelsMatrix { set; get; }

        public TestCommand testCommand { set; get; } 

        public ViewContext()
        {
            testCommand = new TestCommand(this);

            PixelsMatrix = new Pixel[10][];
            for (int Y = 0; Y < 10; Y++)
            {
                PixelsMatrix[Y] = new Pixel[10];
                for (int X = 0; X < 10; X++)
                {
                    PixelsMatrix[Y][X] = new Pixel { Column = X, Row = Y };
                    Pixels.Add(PixelsMatrix[Y][X]);
                }
            }
            test();
        }


        Ship te;
        public void test()
        {
             te = new ShipCruiser(this);
            te.AddShip();

        }

        public void SetShip(Ship ship)
        {
           foreach( var item in ship.ShipBoody)
            {
                PixelsMatrix[item.Row][item.Column].PixelContent = item;
            }
        }
        public void RemoveShip(Ship ship)
        {
            ship = te;
            foreach (var item in ship.ShipBoody)
            {
                PixelsMatrix[item.Row][item.Column].PixelContent = null;
            }
        }
    }
}
