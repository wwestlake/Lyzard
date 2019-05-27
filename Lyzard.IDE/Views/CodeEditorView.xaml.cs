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
using ICSharpCode.AvalonEdit.Folding;
using Lyzard.IDE.Folding;
using Lyzard.IDE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lyzard.IDE.Views
{
    /// <summary>
    /// Interaction logic for CodeEditorView.xaml
    /// </summary>
    public partial class CodeEditorView : UserControl
    {
        private FoldingManager foldingManager;
        private BraceFoldingStrategy braceStrategy;
        private RegionFoldingStrategy regionStrategy;

        public CodeEditorView()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                if (foldingManager == null)
                {
                    foldingManager = FoldingManager.Install(textEditor.TextArea);
                }

                braceStrategy = new BraceFoldingStrategy();
                regionStrategy = new RegionFoldingStrategy();
                textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);

                DispatcherTimer foldingUpdateTimer = new DispatcherTimer();
                foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
                foldingUpdateTimer.Tick += FoldingUpdateTimer_Tick;
                foldingUpdateTimer.Start();
            };
        }

        private void FoldingUpdateTimer_Tick(object sender, EventArgs e)
        {
            

            if (braceStrategy != null && regionStrategy != null)
            {
                braceStrategy.UpdateFoldings(foldingManager, textEditor.Document, regionStrategy);
            }
        }

        private void TextEditor_DocumentChanged(object sender, EventArgs e)
        {
            var vm = DataContext as CodeEditorViewModel;
            if (vm != null)
            {
                vm.IsDirty = true;
            }
        }
    }
}
