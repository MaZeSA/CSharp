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
                    VisulGridBoodyes.Add(new EntyView(i, t));

            AddVisibleObj(new ShipCruiser());
            AddVisibleObj(new ShipFrigate(3,3));
        }

        private List<IVisible> VisibleObjects { set; get; } = new List<IVisible>();
        public ObservableCollection<IBoody> VisulGridBoodyes { set; get; } = new ObservableCollection<IBoody>();
        public GameModel GameModel { get; }

        public void AddVisibleObj(IVisible obj)
        {
            if (VisibleObjects.IndexOf(obj) > -1) return;

            VisibleObjects.Add(obj);
            foreach (var item in obj.VisulBoodies)
                VisulGridBoodyes.Add(item);
        }
        public void RemoveVisibleObj(IVisible obj)
        {
            VisibleObjects.Remove(obj);
            foreach (var item in obj.VisulBoodies)
                VisulGridBoodyes.Remove(item);
        }

        public void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dynamic t = e.OriginalSource;
            IBoody rw = t.DataContext;
            rw.ParentObj.Rotate();
        }


        public void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
            var send = (IBoody)e.Data.GetData("sender");

            if (moved is null) return;
            dynamic t = e.OriginalSource;
            var rw = t.DataContext;
            var r = rw.Row - send.Row;
            var c = rw.Column - send.Column;
            moved.Move(r, c);  
            
            AddVisibleObj(moved);   
            GameModel.GamePreparationModel.DropValidation(moved);
        }


    }
}
