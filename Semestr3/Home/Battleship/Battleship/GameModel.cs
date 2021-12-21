using Battleship.Commands;
using Battleship.ViewModel;
using Battleship.ViewModel.GamePanels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class GameModel
    {
        public MenuControl MenuControl { set; get; }

        public GPanelView GPanelView { set; get; }
        public CommandEndGame CommandEndGame { set; get; }

        public GameModel()
        {
            MenuControl = new MenuControl(this);
            GPanelView = new GPanelView(this);
            CommandEndGame = new CommandEndGame(this);
        }

        public void Restart()
        {
            MenuControl = new MenuControl(this);
            GPanelView = new GPanelView(this);
            (new CommandOpenMenu()).Execute(MenuControl);
        }
    }
}
