using Lyzard.FileSystem;
using Lyzard.IDE.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels.DialogsViewModels
{
    public class NewProjectDlgViewModel : ViewModelBase
    {
        private string _title;

        public NewProjectDlgViewModel()
        {
            Title = "Create Project";
            ProjectPath = CommonFolders.UserProjects;
        }

        internal Action<bool> Completed { get; set; }

        public ICommand Close => new DelegateCommand((x) => {
            Completed?.Invoke(false);
        });

        public ICommand Create => new DelegateCommand((x) => {
            Completed?.Invoke(true);
        });

        public ICommand SelectFolder => new DelegateCommand((x) => {
            var result = DialogManager.SelectFolder();
            if (!string.IsNullOrEmpty(result))
                ProjectPath = result;
        });


        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                FirePropertyChanged();
            }
        }

        private string _projectPath;

        public string ProjectPath
        {
            get { return _projectPath; }
            set { _projectPath = value; FirePropertyChanged(); }
        }


    }
}
