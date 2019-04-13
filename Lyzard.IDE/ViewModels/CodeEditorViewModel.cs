using ICSharpCode.AvalonEdit.Document;
using Lyzard.IDE.Messages;
using Lyzard.Interfaces;
using Lyzard.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lyzard.IDE.ViewModels
{
    public class CodeEditorViewModel : DocumentViewModelBase
    {

        private string _file = "";
        private TextDocument _document = new TextDocument();

        public CodeEditorViewModel()
        {
            Title = "New Editor";
            IconSource = new BitmapImage((new Uri($"pack://application:,,/Resources/Images/Document-1.png")));
        }

        public ICommand Check => new DelegateCommand((x) => {
            var a = _document.Text;
        });

        public TextDocument Document
        {
            get
            {
                return _document;
            }
            set
            {
                _document = value;
                FirePropertyChanged();
            }
        }

        public override bool CanSave(object param)
        {
            return IsDirty;
        }

        public override void Close()
        {
            if (IsDirty)
            {
                switch (ShowMessageBox($"File '{_file}' has changed, do you want to Save the File.", "File Not Saved", MessageBoxButtons.YesNoCancel))
                {
                    case MessageBoxResults.Yes:
                        Save(null);
                        break;
                    case MessageBoxResults.No:

                        break;
                    case MessageBoxResults.Cancel:
                        return;
                }
            }
        }

        public override void Save(object param)
        {
            IsDirty = false;
            //MessageBroker.Instance.Publish(this, new FileSavedMessage());

        }

        public override void SaveAs(object param)
        {
            MessageBroker.Instance.Publish(this, new FileSavedMessage(), (msg) => {
                Console.WriteLine("SaveAs Got Callback");
            });
        }
    }
}
