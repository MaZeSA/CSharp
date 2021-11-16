using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WordOfTanks.Commands;

namespace WordOfTanks
{
    public class GameСontrol : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
      

        public Game Game { get; }

        public GameСontrol(Game game)
        {
            Game = game;
            SetStart();
        }

        ICommand command;
        public ICommand Command 
        {
            set 
            {
                command = value;
                OnNotify();
            }
            get => command;
        }
        
        string textButton = "Start Battle";
        public string TextButton
        {
            set 
            {
                textButton = value;
                OnNotify();
            }
            get => textButton;
        }

        public void SetStop()
        {
            TextButton = "Stop Battle";
            Command = new CommandStopBattle(Game);
        }
        public void SetStart()
        {
            TextButton = "Start Battle";
            Command = new CommandStartGame(Game);
        }

        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
