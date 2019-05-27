/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
    /// <summary>
    /// Manages the display of Dialog boxes
    /// </summary>
    public static class DialogManager
    {

        /// <summary>
        /// Opens a managed file
        /// </summary>
        /// <returns>A managed file</returns>
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

        /// <summary>
        /// Gets a path 
        /// </summary>
        /// <returns>The file path</returns>
        public static string OpenFilePath()
        {
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
                StateManager.SystemState.LastFileOpenLocation = dlg.FileName ;
            }
            return dlg.FileName;
        }


        /// <summary>
        /// Save file as dialog
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <returns>A managed file</returns>
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

        /// <summary>
        /// Selects a folder
        /// </summary>
        /// <returns>The selected folder</returns>
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

        /// <summary>
        /// Selects a folder to manage
        /// </summary>
        /// <returns>A managed folder</returns>
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
