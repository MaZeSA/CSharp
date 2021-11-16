using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksLib;

namespace WordOfTanks
{
    public class Battle
    {
        public Mower MowerLeft { set; get; } = new Mower(new Tank($"T-34")) { SideMow = Mower.Side.Left };
        public Mower MowerRight { set; get; } = new Mower(new Tank($"Pantera")) { SideMow = Mower.Side.Right };
      
        public bool Move(int width)
        {
            var t1 = MowerLeft.Move();
            var t2 = MowerRight.Move();

            return !(width - (t1 + t2) < 190);
        }

        public Tank GetWinner()
        {
            return MowerLeft.Tank ^ MowerRight.Tank; 
        }

        public void KillLoser(Tank winner)
        {
            var mover = winner == MowerLeft.Tank ? MowerRight : MowerLeft;
            mover.Visibility = Visibility.Collapsed;
        }
    }
    public class Mower : INotifyPropertyChanged
    {
        public Tank Tank { set; get; }
        public Side SideMow { set; get; }

        public Mower(Tank tank) => Tank = tank;

        Visibility visibility = Visibility.Visible;
        public Visibility Visibility
        {
            set 
            {
                visibility = value;
                OnNotify();
            }
            get => visibility;
        }

        Thickness thickness = new Thickness(0);
        public Thickness Thickness
        {
            set 
            { 
                thickness = value;
                OnNotify();
            }
            get => thickness;
        }

        int margin = 0;

        public int Move()
        {
            margin += Tank.GetMoveCoeficient() + 1;
            Thickness = SideMow == Side.Left ? new Thickness(margin, 0, 0, 0) : new Thickness(0, 0, margin, 0 );
            return margin;
        }

        public enum Side
        {
            Left,
            Right
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
