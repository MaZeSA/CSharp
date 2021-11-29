﻿using Battleship.ViewModel.GamePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Battleship.ViewModel.Ships
{
    public class ShipFrigate : Ship
    {
        public ShipFrigate() : base()
        {
            Length = 3;

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new ShipView(this) { Row = 0, Column = i, Background= Brushes.Brown });
            }

          //  VisulBoodies[0].Background = Brushes.DeepPink;
        }
        public ShipFrigate( int Row, int Column) : base()
        {
            Length = 3;

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new ShipView(this) { Row = 0 + Row, Column = i + Column, Background = Brushes.Brown });
            }

           // VisulBoodies[0].Background = Brushes.DeepPink;
        }
    }
}
