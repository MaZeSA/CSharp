using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.GamePanels.Pixels;
using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship.ViewModel.Ships
{
    public abstract class Ship : IVisible
    {
        public List<ShipView> Boody { set; get; } = new List<ShipView>();
        public int Length { set; get; }
        public string Name { set; get; }
        public BaseViewModel ViewModel { set; get; }
   
        public Ship(BaseViewModel viewContext)
        {
            ViewModel = viewContext;
        }

        public object GetMeView
        {
            get 
            {
                StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                foreach (var item in Boody)
                    stackPanel.Children.Add(new Label { Background = item.Background, Width=20 });
                return stackPanel;
            }
        }

        public void Remove()
        {
            foreach (var item in Boody)
                ViewModel.RemoveObject(item);
        }

        public void Add()
        {
            foreach (var item in Boody)
            {
                ViewModel.AddObject(this, item.Column, item.Row);
            }
        }

        public IBoody GetBoody(Pixel pixel)
        {
            foreach (IBoody item in Boody)
                if (item.Row == pixel.Row && item.Column == pixel.Column)
                    return item;
            return null;
        }

        public void Move(int param_r, int param_c)
        {
            if (CheckMove(param_r, param_c))
            {
                Remove();
                foreach (var item in Boody)
                {
                    item.Row += param_r;
                    item.Column += param_c;
                }
                Add();
            }
        }
        public bool CheckMove(int param_r, int param_c)
        {
            int[] rColumn = new int[2]
                {
                    Boody.FirstOrDefault().Column + param_c,
                    Boody.LastOrDefault().Column + param_c
                }; 
            int[] rRow = new int[2]
                 {
                    Boody.FirstOrDefault().Row + param_r,
                    Boody.LastOrDefault().Row + param_r,
                 };

            bool res = true;

            foreach (int t in rColumn)
                if (!(t > -1 && t < BaseViewModel.CONST_C))
                    res = false; 
            foreach (int t in rRow)
                if (!(t > -1 && t < BaseViewModel.CONST_R))
                    res = false;

            return res;
        }

        public void Rotate()
        {
            Remove();
            if (Boody.FirstOrDefault().Column == Boody.LastOrDefault().Column)//vertical
            {
                if(Boody.FirstOrDefault().Column > BaseViewModel.CONST_C/2)
                {
                    for (int i = 0; i < Length; i++) //left
                    {
                        Boody[i].Column = Boody[i].Column - i;
                        Boody[i].Row = Boody.FirstOrDefault().Row;
                    }
                }
                else 
                {
                    for (int i = 0; i < Length; i++) //right
                    {
                        Boody[i].Column = Boody[i].Column + i;
                        Boody[i].Row = Boody.FirstOrDefault().Row;
                    }
                }
            }
            else
            {
                if (Boody.FirstOrDefault().Row > BaseViewModel.CONST_R / 2) //up
                {
                    for (int i = 0; i < Length; i++)
                    {
                        Boody[i].Row = Boody[i].Row - i;
                        Boody[i].Column = Boody.FirstOrDefault().Column;
                    }
                }
                else 
                {
                    for (int i = 0; i < Length; i++)
                    {
                        Boody[i].Row = Boody[i].Row + i;
                        Boody[i].Column = Boody.FirstOrDefault().Column;
                    }
                }
            }
            Add();
        }

        public void ChangeParent(BaseViewModel baseViewModel)
        {
            Remove();
            ViewModel.Ships.Remove(this);
            ViewModel = baseViewModel;
            baseViewModel.Ships.Add(this);
            Add();
        }

        public void Click(ShipView sender)
        {
            ChangeParent(ViewModel.GameViewModel.ActionViewModel);
            ViewModel.GameViewModel.ActionViewModel.Selected = this;
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
