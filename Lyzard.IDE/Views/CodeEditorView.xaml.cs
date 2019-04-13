using ICSharpCode.AvalonEdit.Folding;
using Lyzard.IDE.Folding;
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
                foldingManager = FoldingManager.Install(textEditor.TextArea);
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
    }
}
