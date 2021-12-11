using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel
{
    public class ShipController
    {
        public GameModel GameModel { get; }
        public ObservableCollection<Ship> Ships { set; get; } = new ObservableCollection<Ship>();

        public ShipController(GameModel gameModel) 
        {
            GameModel = gameModel;

            try
            {
                Ships.Add(new ShipCruiser(GameModel, 1, 1));
                Ships.Add(new ShipDestroyer(GameModel, 2, 1));
                Ships.Add(new ShipDestroyer(GameModel, 2, 1));
                Ships.Add(new ShipFrigate(GameModel, 2, 1));
                Ships.Add(new ShipFrigate(GameModel, 2, 1));
                Ships.Add(new ShipFrigate(GameModel, 2, 1));
                Ships.Add(new ShipCorvette(GameModel, 1, 1));
                Ships.Add(new ShipCorvette(GameModel, 2, 1));
                Ships.Add(new ShipCorvette(GameModel, 1, 1));
                Ships.Add(new ShipCorvette(GameModel, 2, 1));
            }
            catch { }
        }

        public void CheckCorectPlace()
        {
            foreach (var obj in Ships)
            {
                if (obj.Visual)
                {
                    obj.CheckMove(Ships.Where(x=> x.Visual));
                }
            }
        }

    }
}
