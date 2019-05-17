using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public class DiagramIOViewModel : ViewModelBase
    {
        private string _name;
        private string _value;

        public DiagramIOViewModel()
        {

        }

        public string Name { get { return _name; } set { _name = value; FirePropertyChanged(); } }
        public string Value { get { return _value; } set { _value = value; FirePropertyChanged(); } }

    }
}
