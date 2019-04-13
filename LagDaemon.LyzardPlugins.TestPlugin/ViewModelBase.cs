using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.LyzardPlugins.TestPlugin
{
    public abstract class ViewModelBase : MarshalByRefObject, INotifyPropertyChanged
    {
 
        public event PropertyChangedEventHandler PropertyChanged;

 
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
