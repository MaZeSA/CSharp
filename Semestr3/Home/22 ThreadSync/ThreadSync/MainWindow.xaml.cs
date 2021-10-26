using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThreadSync
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int count = 20;
        Result Result = new Result(count);
        public MainWindow()
        {
            InitializeComponent();
            ListBox1.ItemsSource = Result.ListResult;

            Thread t1 = new Thread(GetFactorial);
            Thread t2 = new Thread(GetPow);
            Thread t3 = new Thread(GetFibonacci);
            t1.Start();
            t2.Start();
            t3.Start();
        }

        private void GetFactorial()
        {
            for (int i = 1; i < count; i++)
            {
                Result.SetFactorial(i, Factorial(i));
            }
        }
        private int Factorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * Factorial(x - 1);
            }
        }

        private void GetPow()
        {
            for (int i = 1; i <= count; i++)
            {
                Result.SetPow(i, (long)Math.Pow(i, 2));

            }
        }

        private void GetFibonacci()
        {
            for (int i = 1; i < count; i++)
            {
                Result.SetFibonacci(i, Fibonacci(i));
            }
        }
        public long Fibonacci(int n)
        {
            return n <= 0 ? 0 : n == 1 ? 1 : Fibonacci(n - 1) + Fibonacci(n - 2);
        }

    }
}
