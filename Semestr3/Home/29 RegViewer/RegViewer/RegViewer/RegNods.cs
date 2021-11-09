using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegViewer
{
    public class RegNode : INotifyPropertyChanged
    {
        public ObservableCollection<RegNode> RegNodes { get; set; } = new ObservableCollection<RegNode>();
        public RegistryKey RegistryKey { get; set; }

        public string Name { get; set; }

        public void ReadChildren()
        {
            try
            {
                var items = RegistryKey?.GetSubKeyNames();

                foreach (var item in items)
                {
                    try
                    {
                        RegNodes.Add(new RegNode { RegistryKey = RegistryKey.OpenSubKey(item), Name = item });
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get
            {
                ReadChildren();
                return this.isExpanded;
            }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;

                    NotifyPropertyChanged("IsExpanded");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

}
