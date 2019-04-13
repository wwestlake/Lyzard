using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;

namespace Lyzard.PluginFramework
{
    public interface IMainRibbonApi
    {
        RibbonTab AddTabToRibbon(string header, object dataContext);

    }

 

}
