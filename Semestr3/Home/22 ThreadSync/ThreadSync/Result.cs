using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSync
{
    public class Result //: INotifyPropertyChanged
    {
        // public event PropertyChangedEventHandler PropertyChanged;

       public  ObservableCollection<ResultItem> ListResult { set; get; }

        public Result(int count)
        {
            ListResult = new System.Collections.ObjectModel.ObservableCollection<ResultItem>();
            for (int i = 0; i < count; i++)
                ListResult.Add(new ResultItem(i+1));
        }

        public void SetFibonacci(int index, long value)
        {
            ListResult[index - 1].Fibonacci = value;
        }
        public void SetFactorial(int index, long value)
        {
            ListResult[index - 1].Factorial = value;
        }
        public void SetPow(int index, long value)
        {
            ListResult[index - 1].Pow = value;
        }

        public class ResultItem
        {
            public int Counter { set; get; }
            public long Factorial { set; get; }
            public long Pow { set; get; }
            public long Fibonacci { set; get; }

            public ResultItem(int c) => Counter = c;
        }
    }
}
