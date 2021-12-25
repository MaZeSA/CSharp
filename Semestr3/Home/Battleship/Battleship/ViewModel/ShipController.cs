using Battleship.ViewModel.Ships;
using System.Collections.ObjectModel;
using System.Linq;
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
                Ships.Add(new ShipCruiser(GPanelView));
                Ships.Add(new ShipDestroyer(GPanelView));
                Ships.Add(new ShipDestroyer(GPanelView));
                Ships.Add(new ShipFrigate(GPanelView));
                Ships.Add(new ShipFrigate(GPanelView));
                Ships.Add(new ShipFrigate(GPanelView));
                Ships.Add(new ShipCorvette(GPanelView));
                Ships.Add(new ShipCorvette(GPanelView));
                Ships.Add(new ShipCorvette(GPanelView));
                Ships.Add(new ShipCorvette(GPanelView));
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
