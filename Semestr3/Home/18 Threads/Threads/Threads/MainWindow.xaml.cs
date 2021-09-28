using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Threads
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Thread> threads { set; get; }

        public MainWindow()
        {
            InitializeComponent();

            threads = new List<Thread>
            {
                new Thread(GetFactorial),
                new Thread(GetPrimeNumbers),
                new Thread(GetFibonacci)
            };

            foreach(var t in threads)
            {
                t.Start(15);
            }

            Thread thread = new Thread(Vait) {IsBackground = true };
            thread.Start();
        }

        private void Vait()
        {
            Thread.Sleep(3000);
            foreach (var t in threads)
            {
                t.Join();
            }

            Environment.Exit(0);
        }

        private void GetFactorial(object n)
        {
            string result = Factorial((int)n).ToString();
            txtFactorial.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => txtFactorial.Text = result));
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

        private void GetPrimeNumbers(object input)
        {
            string result = "";
            for (int i = 2; i <= (int)input; i++)
            {
                int j;
                for (j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        break;
                    }
                }
                if (j > Math.Sqrt(i))
                {
                    result += i + " ";
                }

            } 
            txtPrimeNumbers.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => txtPrimeNumbers.Text = result));
        }
    
        private void GetFibonacci(object n)
        {
            string result = "";
           
            for (int i = 0; i < (int)n; i++)
            {
                result += Fibonacci(i).ToString() + " ";
            } 
         
            txtFibonacci.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => txtFibonacci.Text = result));
        }
        public long Fibonacci(int n)
        {
            return n <= 0 ? 0 : n == 1 ? 1 : Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var t in threads)
            {
                t.Abort();
            }
        }
    }
}
