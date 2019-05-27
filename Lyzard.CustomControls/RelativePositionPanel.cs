/*
 * sukram: The Code Project Open License (CPOL)
 * https://www.codeproject.com/Articles/22952/WPF-Diagram-Designer-Part-1
 */

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

namespace Lyzard.CustomControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Lyzard.CustomControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Lyzard.CustomControls;assembly=Lyzard.CustomControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:RelativePositionPanel />
    ///
    /// </summary>
    public class RelativePositionPanel : Panel
    {

        /// <summary>
        /// Dependency Property the holds the position on the canvas
        /// </summary>
        public static readonly DependencyProperty RelativePositionProperty =
                DependencyProperty.RegisterAttached("RelativePosition", typeof(Point), typeof(RelativePositionPanel),
                    new FrameworkPropertyMetadata(new Point(0, 0),
                        new PropertyChangedCallback(RelativePositionPanel.OnRelativePositionChanged)));

 
        /// <summary>
        /// Gets the relative position of the UI Element
        /// </summary>
        /// <param name="element">The UI element to get relative position</param>
        /// <returns>The point relative to the parent panel</returns>
        public static Point GetRelativePosition(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (Point)element.GetValue(RelativePositionProperty);
        }


        /// <summary>
        /// Sets the relative position of the UI Element
        /// </summary>
        /// <param name="element">The element to set</param>
        /// <param name="value">The relative position</param>
        public static void SetRelativePosition(UIElement element, Point value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(RelativePositionProperty, value);
        }

        /// <summary>
        /// Called when the relative position changes
        /// </summary>
        /// <param name="d">The calling object</param>
        /// <param name="e">The events args</param>
        private static void OnRelativePositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement refer = d as UIElement;
            if (refer != null)
            {
                RelativePositionPanel parent = VisualTreeHelper.GetParent(refer) as RelativePositionPanel;
                if (parent != null)
                {
                    parent.InvalidateArrange();
                }
            }
        }

        /// <summary>
        /// Called when parents wants to arrange the children
        /// </summary>
        /// <param name="arrangeSize">The size to arrange into</param>
        /// <returns>The arrange size</returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement element in InternalChildren)
            {
                if (element != null)
                {
                    Point position = GetRelativePosition(element);
                    double x = (arrangeSize.Width - element.DesiredSize.Width) * position.X;
                    double y = (arrangeSize.Height - element.DesiredSize.Height) * position.Y;

                    if (double.IsNaN(x)) x = 0;
                    if (double.IsNaN(y)) y = 0;

                    element.Arrange(new Rect(new Point(x, y), element.DesiredSize));
                }
            }
            return arrangeSize;
        }

        /// <summary>
        /// Called to measure teh children components
        /// </summary>
        /// <param name="availableSize">The available size</param>
        /// <returns>The measure size</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach (UIElement element in this.InternalChildren)
            {
                if (element != null) element.Measure(size);
            }

            return base.MeasureOverride(availableSize);
        }

    }
}
