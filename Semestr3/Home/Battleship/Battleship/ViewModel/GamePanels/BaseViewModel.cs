using Battleship.Commands;
using Battleship.ViewModel.GamePanels.Pixels;
using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public const int CONST_C = 10;
        public const int CONST_R = 10;
        public GameView GameViewModel { set; get; }

        public ObservableCollection<Pixel> Pixels { set; get; } = new ObservableCollection<Pixel>();
        public Pixel[][] PixelsMatrix { set; get; }

        //public List<Ship> Ships { set; get; } = new List<Ship>();
        public ObservableCollection<Ship> Ships { set; get; } = new ObservableCollection<Ship>();

        public TestCommand testCommand { set; get; }


        public BaseViewModel(GameView gameView)
        {
            GameViewModel = gameView;
            testCommand = new TestCommand(this);

            PixelsMatrix = new Pixel[CONST_R][];
            for (int row = 0; row < CONST_R; row++)
            {
                PixelsMatrix[row] = new Pixel[CONST_C];
                for (int column = 0; column < CONST_C; column++)
                {
                    PixelsMatrix[row][column] = new Pixel { Column = column, Row = row};
                    Pixels.Add(PixelsMatrix[row][column]);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public void AddObject(IVisible obj, int column, int row)
        {
            PixelsMatrix[row][column].PixelContent = obj;
        }
        public void RemoveObject(IBoody obj)
        { 
            PixelsMatrix[obj.Row][obj.Column].PixelContent = null;
        }

       public void SetColorAllPixels(SolidColorBrush brush)
        {
            foreach (var pix in Pixels)
                pix.Background = brush;
        }
    }
}
