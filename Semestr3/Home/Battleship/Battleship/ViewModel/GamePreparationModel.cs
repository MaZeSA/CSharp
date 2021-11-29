using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship.ViewModel
{
    public class GamePreparationModel
    {
        public GameModel GameModel { get; }
        public ObservableCollection<IVisible> Ships { set; get; } = new ObservableCollection<IVisible>();

        public GamePreparationModel(GameModel gameModel)
        {
            GameModel = gameModel;

            Ships.Add(new ShipCruiser());
            Ships.Add(new ShipFrigate()); 
            Ships.Add(new ShipFrigate());
        }

        public void DropValidation(IVisible visible)
        {
            Ships.Remove(visible);
        }

        public void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
            var send = (IBoody)e.Data.GetData("sender");
          
            if (moved is null) return;
            if (Ships.IndexOf(moved) > -1) return;

            GameModel.VisualElementsModel.RemoveVisibleObj(moved);
            Ships.Add(moved);
        }
    }
}
