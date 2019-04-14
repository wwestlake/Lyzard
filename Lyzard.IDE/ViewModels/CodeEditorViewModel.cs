using ICSharpCode.AvalonEdit.Document;
using Lyzard.FileSystem;
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

        private TextDocument _document = new TextDocument();
        private ManagedFile _file;

        public CodeEditorViewModel()
        {
            Title = "New Editor";
            IconSource = new BitmapImage((new Uri($"pack://application:,,/Resources/Images/Document-1.png")));
        }

        public CodeEditorViewModel(string path)
        {
            _file = ManagedFile.Create(path);
            if (_file != null)
            {
                Document.Text = _file.Load();
                Title = _file.FileName;
                ContentId = $"file://{_file.FullPath}";
                //FirePropertyChanged("Document");
            }
        }

        public CodeEditorViewModel(ManagedFile file)
        {
            _file = file;
            if (_file != null)
            {
                Document.Text = _file.Load();
                Title = _file.FileName;
                ContentId = $"file://{_file.FullPath}";
                //FirePropertyChanged("Document");
            }
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
            if (!IsDirty)
            {
                DockManagerViewModel.DocumentManager.Documents.Remove(this);
            } else
            {
                //dialog
            }
        }

        public override void Save(object param)
        {
            IsDirty = false;

        }

        public override void SaveAs(object param)
        {
        }
    }
}
