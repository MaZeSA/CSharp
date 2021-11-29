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
        public GamePreparationModel GamePreparationModel { set; get; }
       
        CommandClick CommandClick { set; get; }

        public GameModel()
        {
            VisualElementsModel = new VisualElementsModel(this);
            GamePreparationModel = new GamePreparationModel(this);  
            
            CommandClick = new CommandClick(this);
        }


    }
}
