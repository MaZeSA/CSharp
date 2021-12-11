using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.GamePanels.Pixels;
using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Battleship.ViewModel.Ships
{
    public abstract class Ship : BaseVisualElement
    {

        public int Length { set; get; }
        public string Name { set; get; }

        public Ship(GameModel gameModel) : base(gameModel)
        {
            VisulBoodies = new List<IBoody>();
        }

        public object GetMeView
        {
            get
            {
                StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Background = Brushes.Red };
                //foreach (var item in VisulBoodies)
                //    stackPanel.Children.Add(new Label { Background = item.Background, Width=20 });
                return stackPanel;
            }
        }

        public override void Move(int param_r, int param_c)
        {
            this.Column = param_c;
            this.Row = param_r;
        }

        public void CheckMove(IEnumerable<Ship> listObj)
        {
            WrongBorder = Visibility.Collapsed;

            foreach (IVisible obj in listObj)
            {
                if (this == obj) continue;

                bool brow = false;

                List<int> rows = new List<int>();

                for (int i = Row - 1; i < Row + RowSpan + 1; i++)
                    rows.Add(i);

                foreach (var r in obj.VisulBoodies)
                {
                    if (rows.IndexOf(r.Row) > -1)
                    {
                        brow = true;
                        break;
                    }
                }

                if (brow == true)
                {
                    brow = false;
                    rows.Clear();
                    for (int i = Column - 1; i < Column + ColumnSpan + 1; i++)
                        rows.Add(i);

                    foreach (var r in obj.VisulBoodies)
                    {
                        if (rows.IndexOf(r.Column) > -1)
                        {
                            brow = true;
                            break;
                        }
                    }

                    if (brow == true)
                    {
                        WrongBorder = Visibility.Visible;
                        break;
                    }
                }
            }

            if (this.ColumnSpan > 1)
            {
                if (VisualElementsModel.CONST_C - this.Column < this.Length)
                    WrongBorder = Visibility.Visible;
            }
            else
            {
                if (VisualElementsModel.CONST_R - this.Row < Length)
                    WrongBorder = Visibility.Visible; ;
            }
        }


        bool rotate = false;
        public override void Rotate()
        {
            int temp = 0;
            temp = RowSpan;
            RowSpan = ColumnSpan;
            ColumnSpan = temp;

            if (rotate)
            {
                ImageSource = new BitmapImage(BitmapUri);
            }
            else
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = BitmapUri;
                bi.Rotation = Rotation.Rotate90;
                bi.EndInit();
                ImageSource = bi;
            }
            rotate = !rotate;

            GameModel.ShipController.CheckCorectPlace();
        }
        public override void IVisible_MouseEnter(object sender, MouseEventArgs e) 
        {
            PopupIsOpen = Visibility.Visible;
        }
        public override void IVisible_MouseLeave(object sender, MouseEventArgs e) 
        {
            PopupIsOpen = Visibility.Collapsed;
        }

        public override void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject();
            data.SetData("Object", this);

            DragDrop.DoDragDrop(sender as DependencyObject, data, DragDropEffects.All);
        }
        public override void SetVisual(bool state)
        {
            Visual = state;
            if (!state && rotate)
                Rotate();
        }
    }
}
