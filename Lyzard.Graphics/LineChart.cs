using System;
using System.Drawing;

namespace Lyzard.GraphicsLib
{
    public class LineChart : Chart
    {
        public LineChart(GraphDescription description) : base(description)
        {
        }

        protected override Bitmap RenderChart<T>(Graphics g, DataSet<T> dataset)
        {
            throw new NotImplementedException();
        }
    }
}
