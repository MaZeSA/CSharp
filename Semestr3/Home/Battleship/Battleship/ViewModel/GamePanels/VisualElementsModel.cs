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
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels
{
    public class VisualElementsModel : INotifyPropertyChanged
    {
        public const int CONST_C = 10;
        public const int CONST_R = 10;
        public ObservableCollection<IVisible> VisibleObjects { set; get; } = new ObservableCollection<IVisible>();
        public GPanelView GPanelView { get; }
          
        public void Shot(int row, int column)
        {
            //foreach (var r in GPanelView.ShipController.Ships)
            //{
            //    var t = r.Shot(row, column);
            //    if (t) return;
            //}

            foreach (var r in VisibleObjects)
            {
                r.Shot(row, column);
            }
        }

        public VisualElementsModel(GPanelView gPanelView)
        {
            GPanelView = gPanelView;

            for (int i = 0; i < CONST_R; i++)
                for (int t = 0; t < CONST_C; t++)
                    VisibleObjects.Add(new EntityPixel(gPanelView, i, t));

            var BitmapUri = new Uri("/Battleship;component/Resources/vzriv.png", UriKind.RelativeOrAbsolute);

            VisibleObjects[33].ImageSource = new System.Windows.Media.Imaging.BitmapImage(BitmapUri);
        }
        public void AddVisibleObj(IVisible obj)
        {
            if (VisibleObjects.IndexOf(obj) > -1) return;
            VisibleObjects.Add(obj);
        }
        public void RemoveVisibleObj(IVisible obj)
        {
            VisibleObjects.Remove(obj);
            obj.SetVisual(false);
        }

        public void UIElement_GridOnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
            if (moved is null) return;

            moved.SetVisual(true);

            VisibleObjects.Remove(moved);
            VisibleObjects.Add(moved);

            GPanelView.ShipController.CheckCorectPlace();
        }


        Visibility blackEnemyPanelVisibility = Visibility.Collapsed;
        public Visibility BlackEnemyPanelVisibility
        {
            set { blackEnemyPanelVisibility = value; OnNotify(); }
            get => blackEnemyPanelVisibility;
        }

        string blackPanelEnemyText = "Waiting Enemy...";
        public string BlackPanelEnemyText
        {
            set
            {
                blackPanelEnemyText = value; OnNotify();
            }
            get => blackPanelEnemyText;
        }
        SolidColorBrush backgroundBrush = Brushes.Gray;
        public virtual SolidColorBrush BackgroundBrush
        {
            get => backgroundBrush;
            set { backgroundBrush = value; OnNotify(); }
        }

        public void ClientConnect()
        {
            BlackPanelEnemyText = "Fleet training...";
            BackgroundBrush = Brushes.LightSeaGreen;
        }
        public void WaitingClientConnect()
        {
            BlackEnemyPanelVisibility = Visibility.Visible;
            BlackPanelEnemyText = "Waiting Enemy...";
            BackgroundBrush = Brushes.Gray;
        }
        public void ClientReady()
        {
            BlackEnemyPanelVisibility = Visibility.Visible;
            BlackPanelEnemyText = "Ready!";
            BackgroundBrush = Brushes.LightGreen;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
