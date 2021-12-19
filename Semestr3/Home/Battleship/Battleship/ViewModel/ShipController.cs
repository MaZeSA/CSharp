using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.ViewModel
{
    public class ShipController
    {
        public GPanelView GPanelView { get; }
        public ObservableCollection<Ship> Ships { set; get; } = new ObservableCollection<Ship>();


        public ShipController(GPanelView gPanelView) 
        {
            GPanelView = gPanelView;

            try
            {
                Ships.Add(new ShipCruiser(GPanelView, 1, 1));
                //Ships.Add(new ShipDestroyer(GPanelView, 2, 1));
                //Ships.Add(new ShipDestroyer(GPanelView, 2, 1));
                //Ships.Add(new ShipFrigate(GPanelView, 2, 1));
                //Ships.Add(new ShipFrigate(GPanelView, 2, 1));
                //Ships.Add(new ShipFrigate(GPanelView, 2, 1));
                //Ships.Add(new ShipCorvette(GPanelView, 1, 1));
                //Ships.Add(new ShipCorvette(GPanelView, 2, 1));
                //Ships.Add(new ShipCorvette(GPanelView, 1, 1));
                //Ships.Add(new ShipCorvette(GPanelView, 2, 1));
            }
            catch { }
        }

        public bool CheckCorectPlace()
        {
            bool Ready = true;
            foreach (var obj in Ships)
            {
                if (obj.Visual)
                {
                    var status = obj.CheckMove(Ships.Where(x => x.Visual));

                    Ready = Ready != false && status;
                }
                else
                    Ready = false;
            }

            CommandManager.InvalidateRequerySuggested();
            return Ready;
        }

    }
}
