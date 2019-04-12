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
    public abstract class ViewModelBase : MarshalByRefObject, INotifyPropertyChanged, IPluginDocumentViewModel
    {
        public abstract bool CanClose { get; set; }
        public abstract string Title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract bool CanSave(object param);
        public abstract void Close();

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public abstract void Save(object param);
        public abstract void SaveAs(object param);
    }
}
