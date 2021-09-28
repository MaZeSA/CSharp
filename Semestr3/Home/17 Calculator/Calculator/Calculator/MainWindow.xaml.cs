using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
        }

        enum Mode
        {
            SetOperand_1,
            SetOperand_2,
            ShowResult
        }

        Mode mode { set; get; } = Mode.SetOperand_1;

        string op1;
        string operand1
        {
            set
            {
                op1 = value;
                mode = Mode.SetOperand_2;
            }
            get { return op1; }
        }
        string operand2 { set; get; }
        Key keyOperator { set; get; }
        char viewOperator
        {
            get
            {
                switch (keyOperator)
                {
                    case Key.Add: return '+';
                    case Key.Subtract: return '-';
                    case Key.Divide: return '/';
                    case Key.Multiply: return '*';
                    default: return ' ';
                }
            }
        }
        private void Buttons_on_Panel_Click(object sender, RoutedEventArgs e)
        {
            textBoxDisplay.Focus();
            Emulation.KeyDown(Convert.ToByte((sender as Button).Tag.ToString()));
        }

        private void textBoxDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (mode == Mode.ShowResult)
                        {
                            textBlock_operand.Text += textBoxDisplay.Text;
                        }  
        
            switch (e.Key)
            {
                case Key.Add:
                case Key.Subtract:
                case Key.Divide:
                case Key.Multiply:
                    {
                        if (mode == Mode.SetOperand_2)
                        {
                            ShowResult();
                        }
                        SetFirstOperator(e.Key);
                        e.Handled = true;
                        break;
                    }
                case Key.Decimal:
                    {
                        e.Handled = SetDecimal();
                        break;
                    }
                case Key.Enter:
                    {
                        Button_Click_Enter2(null, null);
                        break;
                    }
                default:
                    {
                        if (textBoxDisplay.Text == "0" || mode == Mode.ShowResult)
                        {
                            textBoxDisplay.Text = "";
                            if (mode == Mode.ShowResult) e.Handled = true;
                        }
                        break;
                    }
            }
            textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
        }

        private void SetFirstOperator(Key key)
        {
            operand1 = decimal.Parse(textBoxDisplay.Text.Replace('.', ',')).ToString(); 
            keyOperator = key;
            textBlock_operand.Text = operand1.ToString() + viewOperator;
            textBoxDisplay.Text = "0";
            textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
            mode = Mode.SetOperand_2;
        }
        private void SetSecondOperator()
        {
            operand2 = decimal.Parse(textBoxDisplay.Text.Replace('.', ',')).ToString();
            textBlock_operand.Text = (operand1 + viewOperator + operand2).Replace(',','.');
            mode = Mode.ShowResult;
        }
        private bool SetDecimal()
        {
            if (textBoxDisplay.Text == "0" || string.IsNullOrWhiteSpace(textBoxDisplay.Text) || mode == Mode.ShowResult)
            {
                if (mode == Mode.ShowResult) mode = Mode.SetOperand_1;

                textBoxDisplay.Text = "0.";
                return true;
            }
            else if (textBoxDisplay.Text.IndexOf('.') != -1)
            {
                return true;
            }

            return false;
        }

        private void Button_DecimalReverse(object sender, RoutedEventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text.StartsWith("-") == true ? textBoxDisplay.Text.Remove(0, 1) : "-" + textBoxDisplay.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBoxDisplay.Text = "0";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textBlock_operand.Text = "";
            textBoxDisplay.Text = "0";
            mode = Mode.SetOperand_1;
        }

        private void Button_Click_Enter2(object sender, RoutedEventArgs e)
        {
            if (mode == Mode.SetOperand_2)
            {
                ShowResult();
            }
        }

        private void ShowResult()
        {
                SetSecondOperator();
            dynamic result = Result();

            try
            {
                if (result == Math.Truncate(result))
                {
                    result = Convert.ToInt64(result);
                }
            }
            catch
            {}

            textBoxDisplay.Text = result.ToString("#.############").Replace(',','.');
            textBlock_operand.Text += "=";
        }
        decimal Result()
        {
            decimal o1 = decimal.Parse(operand1);
            decimal o2 = decimal.Parse(operand2);
            try
            {
                switch (keyOperator)
                {
                    case Key.Add:
                        return decimal.Add(o1, o2);
                    case Key.Subtract:
                        return decimal.Subtract(o1, o2);
                    case Key.Divide:
                        {
                            if (o2 == 0m) return decimal.Zero;
                            return decimal.Divide(o1, o2);
                        }
                    case Key.Multiply:
                        return decimal.Multiply(o1, o2);
                }
                return decimal.MinValue;
            }
            catch(Exception ex) { textBlock_operand.Text = ex.Message; return 0; }
        }


        private void textBoxDisplay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && (e.Text == "." && textBoxDisplay.Text.IndexOf('.') > -1))
            {  
                e.Handled = true;
            }
          
        }

        //private delegate decimal OperationDelegate(decimal x, decimal? y);
        //private Dictionary<string, OperationDelegate> _operations;

        //public void Calculator()
        //{
        //    _operations = new Dictionary<string, OperationDelegate>
        //    {
        //         { "+", (x, y) =>  x + y.Value},
        //         { "-", (x, y) => x - y.Value },
        //         { "*", (x, y) => x * y.Value },
        //         { "/", (x, y) => y.Value !=0? x / y.Value : 0 },
        //    };

        //}
    }
}
