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
using System;
/* 
 * Lyzard Code Generation System
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.FileSystem
{
    public static class FileSystemAccess
    {

        public static IEnumerable<FileSystemItem> GetDrives()
        {
            return Directory.GetLogicalDrives().ToList().Select(drive => {
                return new FileSystemItem
                {
                    FullPath = drive,
                    IsHidden = false,
                    IsSystem = false,
                    Name = drive,
                    ItemType = FileSystemItemType.Drive,
                };
            });
        }

        public static IEnumerable<FileSystemItem> GetFolders(string path)
        {
            return Directory.GetDirectories(path).ToList().Select(folder => {
                var di = new DirectoryInfo(folder);

                return new FileSystemItem
                {
                    FullPath = folder,
                    IsHidden = (di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden,
                    IsSystem = (di.Attributes & FileAttributes.System) == FileAttributes.System,
                    Name = GetFileOrFolderName(folder),
                    ItemType = FileSystemItemType.Folder,
                    CreatedOn = di.CreationTime,
                    ModfiedOn = di.LastWriteTime,
                    Accessible = HasWritePermission(folder)
                };
            });
        }

        public static IEnumerable<FileSystemItem> GetFiles(string path)
        {
            return Directory.GetFiles(path).ToList().Select(file => {
                var di = new FileInfo(file);

                return new FileSystemItem
                {
                    FullPath = file,
                    IsHidden = (di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden,
                    IsSystem = (di.Attributes & FileAttributes.System) == FileAttributes.System,
                    Name = GetFileOrFolderName(file),
                    ItemType = FileSystemItemType.File,
                    CreatedOn = di.CreationTime,
                    ModfiedOn = di.LastWriteTime,
                    Accessible = HasWritePermission(file),
                    Extension = GetExtension(file)
                };
            });
        }

        public static IEnumerable<FileSystemItem> GetFolderContents(string fullPath)
        {
            var result = GetFolders(fullPath).ToList();
            result.AddRange(GetFiles(fullPath));
            return result;
        }

        public static string GetExtension(string path)
        {
            var ext = Path.GetExtension(path);
            if (ext.StartsWith("."))
                return ext.Substring(1);
            else
                return ext;

        }

        public static IEnumerable<FileSystemItem> GetFileSystemItems(string path)
        {
            var result = new List<FileSystemItem>();
            result.AddRange(GetFolders(path));
            result.AddRange(GetFiles(path));
            return result;
        }

        private static bool HasWritePermission(string FilePath)
        {
            try
            {
                FileSystemSecurity security;
                if (File.Exists(FilePath))
                {
                    security = File.GetAccessControl(FilePath);
                }
                else
                {
                    security = Directory.GetAccessControl(Path.GetDirectoryName(FilePath));
                }
                var rules = security.GetAccessRules(true, true, typeof(NTAccount));

                var currentuser = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                bool result = false;
                foreach (FileSystemAccessRule rule in rules)
                {
                    if (0 == (rule.FileSystemRights &
                        (FileSystemRights.WriteData | FileSystemRights.Write)))
                    {
                        continue;
                    }

                    if (rule.IdentityReference.Value.StartsWith("S-1-"))
                    {
                        var sid = new SecurityIdentifier(rule.IdentityReference.Value);
                        if (!currentuser.IsInRole(sid))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!currentuser.IsInRole(rule.IdentityReference.Value))
                        {
                            continue;
                        }
                    }

                    if (rule.AccessControlType == AccessControlType.Deny)
                        return false;
                    if (rule.AccessControlType == AccessControlType.Allow)
                        result = true;
                }
                return result;
            }
            catch
            {
                return false;
            }
        }
        public static string GetFileOrFolderName(string path)
        {
            var result = default(string);
            if (!path.Contains("\\")) return path;
            int idx = path.LastIndexOf("\\");
            result = path.Substring(idx + 1);
            return result;
        }

    }
}
