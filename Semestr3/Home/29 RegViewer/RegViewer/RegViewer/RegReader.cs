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
    public class RegReader
    {
        public ObservableCollection<RegNode> RootNodes { get; set; } = new ObservableCollection<RegNode>()
        {
            new RegNode{RegistryKey = Registry.ClassesRoot, Name = Registry.ClassesRoot.Name},
            new RegNode{RegistryKey = Registry.CurrentUser, Name = Registry.CurrentUser.Name},
            new RegNode{RegistryKey = Registry.LocalMachine, Name = Registry.LocalMachine.Name},
            new RegNode{RegistryKey = Registry.Users, Name = Registry.Users.Name},
            new RegNode{RegistryKey = Registry.CurrentConfig, Name = Registry.CurrentConfig.Name},
        };
    }
}

