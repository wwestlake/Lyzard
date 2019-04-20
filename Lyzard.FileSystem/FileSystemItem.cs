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
 */using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Lyzard.FileSystem
{
    public enum FileSystemItemType { Drive, File, Folder }

    public class FileSystemItem
    {
        public FileSystemItemType ItemType { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModfiedOn { get; set; }
        public bool IsSystem { get; set; }
        public bool IsHidden { get; set; }
        public bool Accessible { get; set; }

        public ObservableCollection<FileSystemItem> Contents { get; set; }
            = new ObservableCollection<FileSystemItem>();

        public ImageSource Image { get; set; }
        public bool IsExpanded { get; set; }
    }
}
