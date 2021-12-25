using Battleship.Commands;
using Battleship.ViewModel;
using Battleship.ViewModel.GamePanels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class GameModel: INotifyPropertyChanged
    {
        public MenuControl MenuControl { set; get; }

        GPanelView gPanelView;
        public GPanelView GPanelView 
        {
            set
            {
                gPanelView = value; OnNotify();
            }
            get => gPanelView;
        }

        public CommandEndGame CommandEndGame { set; get; }

        public GameModel()
        {
            MenuControl = new MenuControl(this);
            GPanelView = new GPanelView(this);
            CommandEndGame = new CommandEndGame(this);
        }

        public void Restart()
        {
            // MenuControl = new MenuControl(this);

            GPanelView?.CloseGame(); 
            (new CommandOpenMenu()).Execute(MenuControl);
            GPanelView = new GPanelView(this);

           
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
