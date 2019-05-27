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
namespace Lyzard.PluginFramework
{
    /// <summary>
    /// Taging interface to specify the view model
    /// </summary>
    public interface IPluginViewModel
    {
        bool CanClose { get; set; }
        string Title { get; set; }
    }
    public interface IPluginDocumentViewModel : IPluginViewModel
    {
        bool CanSave(object param);
        void Close();
        void Save(object param);
        void SaveAs(object param);
    }

    public interface IPluginToolPaneViewModel : IPluginViewModel
    {
    }
}