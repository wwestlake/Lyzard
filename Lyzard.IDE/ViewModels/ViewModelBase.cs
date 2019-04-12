using Lyzard.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lyzard.IDE.ViewModels
{
    public class ViewModelBase : MarshalByRefObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MessageBoxResults ShowMessageBox(string text, string caption, MessageBoxButtons buttons)
        {
            return MessageBoxConverter.FromBuiltInResults( MessageBox.Show(text, caption, MessageBoxConverter.ToBuiltinButtons( buttons )) );
        }

    }
}
