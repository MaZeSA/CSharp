using _11_ResourceDictioanary.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _11_ResourceDictioanary
{
    public class Menu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CommandChangeLang CommandChangeLang { set; get; }
        public CommandSetTheme CommandSetTheme { set; get; }
        public ResourceDictionary Resources { get; }
        

        public Menu(ResourceDictionary resources)
        {
            Resources = resources;
            CommandChangeLang = new CommandChangeLang(this);
            CommandSetTheme = new CommandSetTheme(this);
        }

        public void ChangeLang(string lan)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("Properties/" + lan + ".xaml", UriKind.Relative);

            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(dictionary);
        }

        public void ChangeThemes(string th)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("Themes/" + th + ".xaml", UriKind.Relative);
            Resources.MergedDictionaries.Add(dictionary);
        }

    }
}
