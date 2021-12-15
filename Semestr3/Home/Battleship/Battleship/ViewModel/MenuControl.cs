using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship.ViewModel
{
    public class MenuControl : INotifyPropertyChanged
    {
        public MenuControl(GameModel gameModel)
        {
            GameModel = gameModel;
            CreateGameModel = new CreateGameModel(GameModel);
        }

        public GameModel GameModel { get; }
        public CreateGameModel CreateGameModel { set; get; }



        Visibility visibilityWait = Visibility.Collapsed;
        public Visibility VisibilityWait
        {
            set { visibilityWait = value; OnNotify(); }
            get => visibilityWait;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
