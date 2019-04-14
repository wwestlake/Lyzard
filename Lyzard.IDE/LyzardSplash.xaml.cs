using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lyzard.IDE
{
    /// <summary>
    /// Interaction logic for LyzardSplash.xaml
    /// </summary>
    public partial class LyzardSplash : Window
    {
        public LyzardSplash()
        {
            InitializeComponent();
        }

        public double Progress
        {
            get { return ProgressBar.Value; }
            set {
                percentage.Text = "" + (value / ProgressBar.Maximum * 100.0) + "%";
                ProgressBar.Value = value;
            }
        }


    }
}
