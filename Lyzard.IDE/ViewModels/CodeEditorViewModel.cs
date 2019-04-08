using Lyzard.IDE.Messages;
using Lyzard.MessageBus;
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
       

        public CodeEditorViewModel()
        {
            Title = "New Editor";
            IconSource = new BitmapImage((new Uri($"pack://application:,,/Resources/Images/Document-1.png")));
        }

        public override bool CanSave(object param)
        {
            return IsDirty;
        }

        public override void Save(object param)
        {
            IsDirty = false;
            //MessageBroker.Instance.Publish(this, new FileSavedMessage());

        }

        public override void SaveAs(object param)
        {
            var a = param;
        }
    }
}
