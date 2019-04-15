using Lyzard.Config;
using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lyzard.IDE.Dialogs
{
    public static class DialogManager
    {

        public static ManagedFile OpenFile()
        {
            ManagedFile file = null;
            var lastPath = StateManager.SystemState.LastFileOpenLocation;
            if (string.IsNullOrEmpty(lastPath))
                lastPath = CommonFolders.UserProjects;
            var dlg = new OpenFileDialog();
            dlg.InitialDirectory = lastPath;
            dlg.DefaultExt = ".lyzard";
            dlg.Filter = "C Sharp (*.cs)|*.cs|Visual Basic (*.vb)|*.vb|Lyzard Project (*.lyzard)|*.lyzard|All File (*.*)|*.*";
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = ManagedFile.Create(dlg.FileName);
                StateManager.SystemState.LastFileOpenLocation = file.FilePath;
                if (file.Extension == ".lyzard")
                {
                    StateManager.SystemState.RecentProjects.Add(file.FullPath);
                } else
                {
                    StateManager.SystemState.RecentFiles.Add(file.FullPath);
                }
            }
            return file;
        }

        public static ManagedFile SaveFileAs(string text)
        {
            ManagedFile file = null;
            var lastPath = StateManager.SystemState.LastFileOpenLocation;
            if (string.IsNullOrEmpty(lastPath))
                lastPath = CommonFolders.UserProjects;
            var dlg = new SaveFileDialog();
            dlg.InitialDirectory = lastPath;
            dlg.DefaultExt = ".cs";
            dlg.Filter = "C Sharp (*.cs)|*.cs|Visual Basic (*.vb)|*.vb|All File (*.*)|*.*";
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = ManagedFile.Create(dlg.FileName);
                file.Save(text);
                StateManager.SystemState.LastFileOpenLocation = file.FilePath;
                if (file.Extension == ".lyzard")
                {
                    StateManager.SystemState.RecentProjects.Add(file.FullPath);
                }
                else
                {
                    StateManager.SystemState.RecentFiles.Add(file.FullPath);
                }
            }
            return file;
        }

        public static string SelectFolder()
        {
            var dlg = new FolderBrowserDialog();
            dlg.SelectedPath = StateManager.SystemState.LastFileOpenLocation;
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                return dlg.SelectedPath;
            }
            return null;
        }

        public static ManagedFolder SelectManagedFolder()
        {
            var path = SelectFolder();
            if (!string.IsNullOrEmpty(path))
            {
                StateManager.SystemState.LastFileOpenLocation = path;
                return new ManagedFolder(path);
            }
            return null;
        }

    }
}
