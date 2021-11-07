using _11_ResourceDictioanary.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _11_ResourceDictioanary
{
    public class PageSelector : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public CommandSetPage CommandSetPage { set; get; }

        public PageSelector()
        {
            CommandSetPage = new CommandSetPage(this);
        }

        string source = "Pages/About.xaml";
        public string Source
        {
            set 
            {
                source = value;
                NotifyPropertyChanged();
            }
            get => source;
        }
        
        public void SetSource(string page)
        {
            Source = page;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
