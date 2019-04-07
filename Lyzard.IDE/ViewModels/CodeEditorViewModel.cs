using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lyzard.IDE.ViewModels
{
    public class CodeEditorViewModel : DocumentViewModelBase
    {
        private string _title;

        public CodeEditorViewModel()
        {
            Title = "New Editor";
            IconSource = new BitmapImage((new Uri($"pack://application:,,/Resources/Images/Document-1.png")));
        }



        

    }
}
