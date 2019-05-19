using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels.FlowChartViewModels
{
    public class FlowChartProcessViewModel : ViewModelBase
    {
        private string _name;
        private object tag;

        public FlowChartProcessViewModel(object tag)
        {
            this.tag = tag;
            if (tag != null) Name = this.tag.ToString();
        }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    }
}
