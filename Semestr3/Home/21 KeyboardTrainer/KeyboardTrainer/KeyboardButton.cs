using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainer
{
    public class KeyboardButton : Border
    { 
        TextBlock textBb { set; get; }
        public string SetText { set => textBb.Text = value; }   
        public int SetFontSize { set => textBb.FontSize = value; }
        public string Symbol { set; get; }
        public virtual string ShiftSymbol { get => Symbol.ToUpper(); }
        public int RowNumer { set; get; }
        public SolidColorBrush BackColor { set; get; }
     

        public KeyboardButton(string symbol, SolidColorBrush color, int row)
        {
            textBb = new TextBlock { HorizontalAlignment = System.Windows.HorizontalAlignment.Center, VerticalAlignment = System.Windows.VerticalAlignment.Center };
           
            BorderBrush = Brushes.Black;
            BorderThickness = new System.Windows.Thickness(2);
            CornerRadius = new System.Windows.CornerRadius(10);
            Margin = new System.Windows.Thickness(2);
            Background = color;
            Width = 50;
            Height = 50;

            this.Child = textBb;

            BackColor = color;
            SetText = symbol;
            SetFontSize = 30;
            Symbol = symbol;
            RowNumer = row;
        }

        public virtual void Caps(bool caps)
        {
            SetText = caps ? textBb.Text.ToUpper() : textBb.Text.ToLower();
        }
        public void DownKey()
        {
            this.Background = new SolidColorBrush(Colors.LightBlue);
        }
        public void UpKey()
        {
            this.Background = BackColor;
        }
    }

    public class TwoCharKey : KeyboardButton
    {
       public override string ShiftSymbol { get; }
        public TwoCharKey(string symbol, string shiftSymbol, int width, SolidColorBrush brush, int row) : base(symbol, brush, row)
        {
            ShiftSymbol = shiftSymbol;
            Width = width;
        }

        public override void Caps(bool caps)
        {
            SetText = caps ? ShiftSymbol : Symbol;
        }
    }

    public class ControlKey : KeyboardButton
    {
        public ControlKey(string symbol, int width, SolidColorBrush brush, int row) : base(symbol, brush, row)
        {
            Width = width;
            SetFontSize = 15;
        }

        public override void Caps(bool caps)
        {
            return;
        }
    }

}
