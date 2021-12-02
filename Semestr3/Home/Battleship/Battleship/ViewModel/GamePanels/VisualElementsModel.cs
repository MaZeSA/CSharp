using Battleship.Commands;
using Battleship.ViewModel.GamePanels.Pixels;
using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels
{
    public class VisualElementsModel
    {
        public const int CONST_C = 10;
        public const int CONST_R = 10;
             
        public CommandClick CommandClick { set; get; }
        public void Commnd()
        {
            VisibleObjects[0].Rotate();
        }

        
        public VisualElementsModel(GameModel gameModel)
        {
            GameModel = gameModel;
                        
            for (int i = 0; i < CONST_R; i++)
                for (int t = 0; t < CONST_C; t++)
                    VisibleObjects.Add(new EntityPixel(i, t));

            AddVisibleObj(new ShipCruiser(0,0));
            AddVisibleObj(new ShipFrigate(3, 3) { RowSpan = 3, ColumnSpan = 1 });
        }

        public ObservableCollection<IVisible> VisibleObjects { set; get; } = new ObservableCollection<IVisible>();
        //public ObservableCollection<IBoody> VisulGridBoodyes { set; get; } = new ObservableCollection<IBoody>();
        public GameModel GameModel { get; }

        public void AddVisibleObj(IVisible obj)
        {
            if (VisibleObjects.IndexOf(obj) > -1) return;

            VisibleObjects.Add(obj);
            //foreach (var item in obj.VisulBoodies)
            //    VisulGridBoodyes.Add(item);
        }
        public void RemoveVisibleObj(IVisible obj)
        {
            VisibleObjects.Remove(obj);
            //foreach (var item in obj.VisulBoodies)
            //    VisulGridBoodyes.Remove(item);
        }

        public void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            //var moved = (IVisible)e.Data.GetData("Object");
            //if (moved is null) return;
            //if (VisibleObjects.IndexOf(moved) > -1)
            //{ return; }
            //else
            //{
            //    dynamic pixel = e.OriginalSource;
            //    IBoody visible = pixel.DataContext;

            //    var r = visible.Row;
            //    var c = visible.Column;
            //    moved.Move(r, c);
            //}

            //VisibleObjects.Add(moved);

        }
      

    }
}
