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
namespace Lyzard.PluginFramework
{
    /// <summary>
    /// This interface is used by the plugin to create documents
    /// and tool panes by passing back the view which must implement
    /// the IPluginDocumentView or IPluginToolPaneView.
    /// </summary>
    public interface IApplicationApi
    {
        /// <summary>
        /// Creartes a DocumentView in the main UI document 
        /// tab area using the specified view
        /// </summary>
        /// <param name="view">The view to display</param>
        void CreateDocument(IPluginDocumentView view, IPluginDocumentViewModel model);

        /// <summary>
        /// Creates a ToolPane in the tool pane area of the
        /// main window.
        /// </summary>
        /// <param name="view">The view to display</param>
        void CreateToolPane(IPluginToolPaneView view, IPluginToolPaneViewModel model);
    }
}