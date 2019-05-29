using System.Drawing;

namespace Lyzard.GraphicsLib
{
    /// <summary>  
    /// Describes that basic attributes needed to build a graph.  
    /// </summary>
    public class GraphDescription
    {
        /// <summary>
        /// the entire drawing region
        /// </summary>
        public Rectangle Region
        {
            get
            {
                return new Rectangle(0, 0, Width, Height);
            }
        }

        /// <summary>
        /// The width of the graphic image in pixels
        /// </summary>
        public int Width { get; set; } = 512;

        /// <summary>
        /// The height of the graphic image in pixels
        /// </summary>
        public int Height { get; set; } = 512;

        /// <summary>
        /// The X Scale.  Pixels per Unit of Measure
        /// </summary>
        public float ScaleX { get; set; } = 1.0f;

        /// <summary>
        /// The Y Scale.  Pixels per Unit of Measure
        /// </summary>
        public float ScaleY { get; set; } = 1.0f;

        /// <summary>
        /// Text label for the X axis of the graph
        /// </summary>
        public string LabelX { get; set; } = "X";

        /// <summary>
        /// Text label for the Y axis
        /// </summary>
        public string LabelY { get; set; } = "Y";

        /// <summary>
        /// The title of the graph
        /// </summary>
        public string Title { get; set; } = "New Graph";

        /// <summary>
        /// The background color of the entire graph
        /// </summary>
        public Color Background { get; set; } = Color.White;

        /// <summary>
        /// The foreground color of the text on the graph
        /// </summary>
        public Color Foreground { get; set; } = Color.Black;

        /// <summary>
        /// The color of the scale lines
        /// </summary>
        public Color ScaleColor { get; set; } = Color.Black;

        /// <summary>
        /// The number of divisions on the X axis
        /// </summary>
        public int DivisionX { get; set; } = 10;

        /// <summary>
        /// The number of divisions on the Y axis
        /// </summary>
        public int DivisionY { get; set; } = 10;
    }
}
