using Battleship.Commands;
using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Battleship.ViewModel
{
    public abstract class BaseVisualElement : INotifyPropertyChanged, IVisible
    {
        public GPanelView GPanelView { get; set; }
        public CommandIVisibleRotate CommandIVisibleRotate { get; set; }
        public CommandIVisibleRemove CommandIVisibleRemove { get; set; }
        public BaseVisualElement(GPanelView gPanelView)
        {
            GPanelView = gPanelView;
            CommandIVisibleRotate = new CommandIVisibleRotate(this);
            CommandIVisibleRemove = new CommandIVisibleRemove(this);
        }

        public List<IBoody> VisulBoodies { set; get; }

        int column = 0;
        public virtual int Column
        {
            get => column;
            set { column = value; OnNotify(); }
        }
        int row = 0;
        public virtual int Row
        {
            get => row;
            set { row = value; OnNotify(); }
        }
        int columnSpan = 1;
        public virtual int ColumnSpan
        {
            get => columnSpan;
            set { columnSpan = value; OnNotify(); }
        }
        int rowSpan = 1;
        public virtual int RowSpan
        {
            get => rowSpan;
            set { rowSpan = value; OnNotify(); }
        }
        SolidColorBrush backgroundBrush;
        public virtual SolidColorBrush BackgroundBrush
        {
            get => backgroundBrush;
            set { backgroundBrush = value; OnNotify(); }
        }
        SolidColorBrush borderBrush;
        public virtual SolidColorBrush BorderBrush
        {
            get => borderBrush;
            set { borderBrush = value; OnNotify(); }
        }
        Thickness borderThickness;
        public virtual Thickness BorderThickness
        {
            get => borderThickness;
            set { borderThickness = value; OnNotify(); }
        }

        Visibility popupIsOpen = Visibility.Collapsed;
        public Visibility PopupIsOpen
        {
            get => popupIsOpen;
            set { popupIsOpen = value; OnNotify(); }
        }
      
        Visibility wrongBorder = Visibility.Collapsed;
        public Visibility WrongBorder
        {
            get => wrongBorder;
            set { wrongBorder = value; OnNotify(); }
        }

        string testString;
        public string TestString 
        {
            get => testString;
            set { testString = value; OnNotify(); }
        }

        public Uri BitmapUri { get; set; }

        BitmapImage imageSource;
        public BitmapImage ImageSource
        {
            get => imageSource;
            set { imageSource = value; OnNotify(); }
        }

        bool visual;
        public bool Visual
        {
            get => visual;
            set { visual = value; OnNotify(); }
        }
     

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public virtual void Move(int param_r, int param_c) { }
        public virtual void Rotate() { }
        public virtual void UIElement_OnDragEnter(object sender, DragEventArgs e) { }
        public virtual void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GPanelView.Shot(new List<int[]> { new int[2] { this.Row, this.Column } });
        }
        public virtual void IVisible_MouseEnter(object sender, MouseEventArgs e) { }
        public virtual void IVisible_MouseLeave(object sender, MouseEventArgs e) { }
        public virtual void SetVisual(bool state)
        {
            Visual = state;
        }
        public virtual bool Shot(int row, int column)
        {
            throw new NotImplementedException();
        }

        //public virtual void CheckMove(List<IVisible> obj){}
    }
}
