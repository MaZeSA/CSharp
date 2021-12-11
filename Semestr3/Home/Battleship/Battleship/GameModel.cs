using Battleship.Commands;
using Battleship.ViewModel;
using Battleship.ViewModel.GamePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class GameModel
    {
        public VisualElementsModel VisualElementsModel { set; get; }
        public ShipController ShipController { set; get; }

    CommandClick CommandClick { set; get; }

        public GameModel()
        {
            VisualElementsModel = new VisualElementsModel(this);
            ShipController = new ShipController(this);

            CommandClick = new CommandClick(this);
        }


    }
}
