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
using Lyzard.CustomControls;
using Lyzard.IDE.ViewModels;
using Lyzard.IDE.ViewModels.SimulationItemViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lyzard.IDE.Views
{
    /// <summary>
    /// Interaction logic for DiagramView.xaml
    /// </summary>
    internal partial class DiagramView : UserControl
    {
        private Brush _color1 = Brushes.Black;
        private Brush _color2 = Brushes.DarkGray;

        public DiagramView()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                var vm = DataContext as DiagramViewModel;
                vm.RegisterDesigner(Designer);
            };

            Designer.DeleteEvent += Designer_DeleteEvent;
            Designer.ConnectionDeleteEvent += Designer_ConnectionDeleteEvent;

            Designer.DropEvent += (s, e) =>
            {
                var item = s as DesignerItem;
                if (item.Content is Grid)
                {
                    var path = (item.Content as Grid).FindName(item.Name) as Path;
                    var result = SimulationViewModelSelector.SelectViewAndViewModel(path);
                    if (result != null) AssignSimulationViewModeAndView(item, result);
                    return;
                }
                else
                {
                    var path = item.Content as Path;
                    var result = FlowChartViewModelSelector.SelectViewAndViewModel(path);
                    if (result != null) AssignFlowChartViewModeAndView(item, result);
                }
            };
        }

        private void Designer_ConnectionDeleteEvent(object sender, ConnectionDeleteEventArgs args)
        {
            var vmSource = (args.Connection.Source.ParentDesignerItem.Content as Control).DataContext as SimViewModelBase;
            var vmSink = (args.Connection.Sink.ParentDesignerItem.Content as Control).DataContext as SimViewModelBase;
            vmSource.OnDeleteConnection(args.Connection);
            vmSink.OnDeleteConnection(args.Connection);
        }

        private void Designer_DeleteEvent(object sender, DesignerItemDeleteEventArgs args)
        {
            var vm = (args.Item.Content as Control).DataContext as SimViewModelBase;
            vm.OnDelete();
        }

        private static void AssignSimulationViewModeAndView(DesignerItem item, Tuple<ViewModels.ViewModelBase, UserControl> result)
        {
            if (result != null)
            {
                result.Item2.DataContext = result.Item1;
                item.Content = result.Item2;
                item.UpdateLayout();
                item.DataContext = null;
                var connectors = item.GetConnectors();
                foreach (var connector in connectors)
                {
                    connector.ConnectionChanged += Connector_ConnectionChanged;
                }
            }
        }



        private static void Connector_ConnectionChanged(object sender, ConnectionChangedEventArgs args)
        {
            var connector = sender as Connector;
            var item = connector.ParentDesignerItem;
            var internalItem = (item.Content as Control);
            var vm = internalItem.DataContext as SimViewModelBase;
            vm.HandleConnectionAdded(connector);
        }

        private static void AssignFlowChartViewModeAndView(DesignerItem item, Tuple<ViewModels.ViewModelBase, UserControl> result)
        {
            if (result != null)
            {

                result.Item2.DataContext = result.Item1;
                var path = item.Content;
                item.Content = result.Item2;
                (result.Item2.FindName("NewContent") as ContentControl).Content = path;
                item.UpdateLayout();
                item.Selected += Item_Selected;
            }
        }

        private static void Item_Selected(object sender, EventArgs args)
        {
            var item = sender as DesignerItem;
            if (item != null)
            {
                var vm = item.DataContext;
                DockManagerViewModel.DocumentManager.SelectDiagramItem(vm);
            }
        }

        private void ShowGridlines_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            DrawGraph((int)SliderValue.Value, (int)SliderValue.Value, Designer);
            SliderValue.IsEnabled = true;
        }

        private void ShowGridlines_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveGraph(Designer);
            SliderValue.IsEnabled = false;
        }

        private void SliderValue_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (ShowGridlines.IsChecked ?? false)
            {
                DrawGraph((int)SliderValue.Value, (int)SliderValue.Value, Designer);
            }
        }

        

        private void DrawGraph(int yoffSet, int xoffSet, Canvas mainCanvas)
        {
            RemoveGraph(mainCanvas);
            Image lines = new Image();
            lines.SetValue(Panel.ZIndexProperty, -100);
            //Draw the grid
            DrawingVisual gridLinesVisual = new DrawingVisual();
            DrawingContext dct = gridLinesVisual.RenderOpen();
            Pen lightPen = new Pen(_color1, 0.5), darkPen = new Pen(_color2, 1);
            lightPen.Freeze();
            darkPen.Freeze();

            int yOffset = yoffSet,
                xOffset = xoffSet,
                rows = (int)(SystemParameters.PrimaryScreenHeight),
                columns = (int)(SystemParameters.PrimaryScreenWidth),
                alternate = yOffset == 5 ? yOffset : 1,
                j = 0;

            //Draw the horizontal lines
            Point x = new Point(0, 0.5);
            Point y = new Point(SystemParameters.PrimaryScreenWidth, 0.5);

            for (int i = 0; i <= rows; i++, j++)
            {
                dct.DrawLine(j % alternate == 0 ? lightPen : darkPen, x, y);
                x.Offset(0, yOffset);
                y.Offset(0, yOffset);
            }
            j = 0;
            //Draw the vertical lines
            x = new Point(0.5, 0);
            y = new Point(0.5, SystemParameters.PrimaryScreenHeight);

            for (int i = 0; i <= columns; i++, j++)
            {
                dct.DrawLine(j % alternate == 0 ? lightPen : darkPen, x, y);
                x.Offset(xOffset, 0);
                y.Offset(xOffset, 0);
            }

            dct.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)SystemParameters.PrimaryScreenWidth,
                (int)SystemParameters.PrimaryScreenHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(gridLinesVisual);
            bmp.Freeze();
            lines.Source = bmp;

            mainCanvas.Children.Add(lines);
        }

        private void RemoveGraph(Canvas mainCanvas)
        {
            foreach (UIElement obj in mainCanvas.Children)
            {
                if (obj is Image)
                {
                    mainCanvas.Children.Remove(obj);
                    break;
                }
            }
        }

    }
}
