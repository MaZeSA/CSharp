using AutoRun.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoRun
{
    public class RegEdit : INotifyPropertyChanged
    {
        public RegistryKey AutoRunKey { set; get; } 
        public CommandRemove CommandRemove { set; get; }
        public CommandAdd CommandAdd { set; get; }


        string selectedValue;
        public string SelectedValue
        {
            set
            {
                selectedValue = value;
                NotifyPropertyChanged();
            }
            get => selectedValue;
        }
   
        string[] data;
        public string[] Data
        {
            set
            {
                data = value;
                NotifyPropertyChanged();
            }
            get => data;
        }

        public RegEdit()
        {
            CommandRemove = new CommandRemove(this);
            CommandAdd = new CommandAdd(this);

            AutoRunKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            GetListRun();
        }

        public void GetListRun()
        {
            Data = AutoRunKey?.GetValueNames();
        }

        public void Remove()
        {
            AutoRunKey?.DeleteValue(SelectedValue);
            GetListRun();
        }
        public void Add()
        {
            var openFileDialog = new OpenFileDialog { Filter = "EXE|*.exe" };

            if (openFileDialog.ShowDialog() == true)
            {
                AutoRunKey.SetValue(Path.GetFileName(openFileDialog.FileName), openFileDialog.FileName);
            }

            GetListRun();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
