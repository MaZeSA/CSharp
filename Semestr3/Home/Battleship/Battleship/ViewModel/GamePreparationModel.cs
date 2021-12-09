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

            Ships.Add(new ShipCruiser(GameModel.VisualElementsModel, 1, 1));
            Ships.Add(new ShipDestroyer(GameModel.VisualElementsModel, 2, 1));
            Ships.Add(new ShipDestroyer(GameModel.VisualElementsModel, 2, 1));
            Ships.Add(new ShipFrigate(GameModel.VisualElementsModel, 2, 1)); 
            Ships.Add(new ShipFrigate(GameModel.VisualElementsModel, 2, 1));
            Ships.Add(new ShipFrigate(GameModel.VisualElementsModel, 2, 1));
            Ships.Add(new ShipCorvette(GameModel.VisualElementsModel, 1, 1));
            Ships.Add(new ShipCorvette(GameModel.VisualElementsModel, 2, 1)); 
            Ships.Add(new ShipCorvette(GameModel.VisualElementsModel, 1, 1));
            Ships.Add(new ShipCorvette(GameModel.VisualElementsModel, 2, 1));             //Ships.Add(new ShipFrigate(4,4));
        }

        public void DropValidation(IVisible visible)
        {
            Ships.Remove(visible);
        }
              
        public void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
          
            if (moved is null) return;
            if (Ships.IndexOf(moved) > -1) return;

            GameModel.VisualElementsModel.RemoveVisibleObj(moved);
            Ships.Add(moved);
        }
    }
}
