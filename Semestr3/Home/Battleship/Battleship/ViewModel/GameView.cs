using Battleship.Commands;
using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel
{
    public class GameView : INotifyPropertyChanged
    {
        public VisualElementsModel BaseViewModel { set; get; }
        public ObservableCollection<Ship> Ships { set; get; } = new ObservableCollection<Ship>();

      
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public GameView()
        {
            //BaseViewModel = new BaseViewModel(this);

            CreateShips();
        }

        private void CreateShips()
        {
            //ActionViewModel.Ships.Add(new ShipCruiser(ActionViewModel) { Name = "Cruiser" });
            //ActionViewModel.Ships.Add(new ShipCruiser(ActionViewModel, 1, 0) { Name = "Cruiser2" });
            //ActionViewModel.Ships.Add(new ShipFrigate(ActionViewModel, 3, 0) { Name = "Frigate" });
        }

    }
}
