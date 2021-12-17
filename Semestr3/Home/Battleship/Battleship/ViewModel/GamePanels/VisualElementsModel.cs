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
    public class VisualElementsModel : IMenu
    {
        public const int CONST_C = 10;
        public const int CONST_R = 10;
        public ObservableCollection<IVisible> VisibleObjects { set; get; } = new ObservableCollection<IVisible>();
        public GameModel GameModel { get; }
        public CommandClick CommandClick { set; get; }
        public CommandIVisibleRemove CommandIVisibleRemove { set; get; }

        int x;
        public int S_X
        {
            set { x = value; }
            get => x;
        }
        int y;

     

        public int S_Y
        {
            set { y = value; }
            get => y;
        }

        public void Shot()
        {
            foreach(var r in GameModel.ShipController.Ships)
            {
               var t = r.Shot(S_Y, S_X);
                if (t) return;
            }

            foreach (var r in VisibleObjects)
            {
                r.Shot(S_Y, S_X);
            }
        }

        public VisualElementsModel(GameModel gameModel)
        {
            CommandClick = new CommandClick(gameModel);

            GameModel = gameModel;
            CommandIVisibleRemove = new CommandIVisibleRemove(this);

            for (int i = 0; i < CONST_R; i++)
                for (int t = 0; t < CONST_C; t++)
                    VisibleObjects.Add(new EntityPixel(GameModel, i, t));

            var BitmapUri = new Uri("/Battleship;component/Resources/vzriv.png", UriKind.RelativeOrAbsolute);

            VisibleObjects[33].ImageSource =  new System.Windows.Media.Imaging.BitmapImage(BitmapUri);

            VisibleObjects.Add(new ShipCruiser(gameModel, 1, 1)); 
            VisibleObjects.Add(new ShipCorvette(gameModel, 2, 1));
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

            GameModel.ShipController.CheckCorectPlace();
        }


        Visibility visualElementVisibility = Visibility.Collapsed;
        public Visibility VisualElementVisibility
        {
            set { visualElementVisibility = value; OnNotify(); }
            get => visualElementVisibility;
        }
        public void Show()
        {
            VisualElementVisibility = Visibility.Visible;
        }

        public void Back()
        {
            VisualElementVisibility = Visibility.Collapsed;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
