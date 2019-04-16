using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using Lyzard.FileSystem;
using Lyzard.IDE.Dialogs;
using Lyzard.IDE.Messages;
using Lyzard.IDE.ViewModels.DialogsViewModels;
using Lyzard.Interfaces;
using Lyzard.MessageBus;
using Lyzard.SystemIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lyzard.IDE.ViewModels
{
    public class CodeEditorViewModel : DocumentViewModelBase
    {

        private TextDocument _document = new TextDocument();
        private ManagedFile _file;
        private IHighlightingDefinition _highlighting;


        public CodeEditorViewModel()
        {
            Title = "New Editor";
            IconSource = new BitmapImage((new Uri($"pack://application:,,/Resources/Images/Document-1.png")));
            SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".cs");
        }


        public CodeEditorViewModel(string path)
        {
            _file = ManagedFile.Create(path);
            if (_file != null)
            {
                Document.Text = _file.Load();
                ContentId = $"file://{_file.FullPath}";
                SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(
                    _file.Extension
                    );
                Title = _file.FileName + $" - ({SyntaxHighlighting.Name})";
                initialLoad = true;
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
                SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(
                    _file.Extension
                    );
                Title = _file.FileName + $" - ({SyntaxHighlighting.Name})";
                initialLoad = true;
            }
        }

        public IHighlightingDefinition SyntaxHighlighting
        {
            get
            {
                return _highlighting;
            }
            set
            {
                _highlighting = value;
                FirePropertyChanged();
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
                var filename = _file != null ? _file.FileName : "New File";

                var vm = new LyzardMessageDlgViewModel();
                DockManagerViewModel.DocumentManager.ShowDialog(vm);
                vm.Title = "Save File";
                vm.TextVisible = true;
                vm.ListVisible = false;
                vm.Message = $"The file '{filename}' is not saved, do you want to save it?";
                vm.YesVisible = true;
                vm.NoVisible = true;
                vm.CancelVisible = true;
                vm.OkVisible = false;
                vm.Completed = CloseDecision;
            }
        }

        public void CloseDecision(DialogViewModelBase dlg)
        {
            var vm = dlg as LyzardMessageDlgViewModel;
            if (vm != null)
            {
                switch (vm.Result)
                {
                    case LyzardMessageResult.Close:
                        DockManagerViewModel.DocumentManager.HideDialog();
                        break;
                    case LyzardMessageResult.Yes:
                        DockManagerViewModel.DocumentManager.HideDialog();
                        DockManagerViewModel.DocumentManager.Documents.Remove(this);
                        if (_file != null)
                            Save(null);
                        else
                            SaveAs(null);
                        break;
                    case LyzardMessageResult.No:
                        DockManagerViewModel.DocumentManager.HideDialog();
                        DockManagerViewModel.DocumentManager.Documents.Remove(this);
                        break;
                    case LyzardMessageResult.Ok:
                        break;
                    case LyzardMessageResult.Cancel:
                        DockManagerViewModel.DocumentManager.HideDialog();
                        break;

                }
            }
        }

        public override void Save(object param)
        {
            if (_file != null)
            {
                 _file.Save(_document.Text);
            } else
            {
                SaveAs(null);
            }

        }

        public override void SaveAs(object param)
        {
            _file = DialogManager.SaveFileAs(_document.Text);
        }
    }
}
