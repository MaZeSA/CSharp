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
    public class VisualElementsModel
    {
        public const int CONST_C = 10;
        public const int CONST_R = 10;
        public ObservableCollection<IVisible> VisibleObjects { set; get; } = new ObservableCollection<IVisible>();
        public GameModel GameModel { get; }
        public CommandClick CommandClick { set; get; }
        public CommandIVisibleRemove CommandIVisibleRemove { set; get; }

        public VisualElementsModel(GameModel gameModel)
        {
            GameModel = gameModel;
            CommandIVisibleRemove = new CommandIVisibleRemove(this);

            for (int i = 0; i < CONST_R; i++)
                for (int t = 0; t < CONST_C; t++)
                    VisibleObjects.Add(new EntityPixel(GameModel, i, t));

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
    }
}
