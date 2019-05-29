using System;
using System.Drawing;

namespace Lyzard.GraphicsLib
{
    public abstract class Renderer
    {
        public abstract void Render<T>(Graphics graphics, GraphDescription description, DataSet<T> dataset);
    }

    public class SolidBackgroundRenderer : Renderer
    {
        public override void Render<T>(Graphics graphics, GraphDescription description, DataSet<T> dataset)
        {
            var brush = new SolidBrush(description.Background);
            graphics.FillRectangle(brush, description.Region);
        }
    }

    public class AxisRender : Renderer
    {
        public override void Render<T>(Graphics graphics, GraphDescription description, DataSet<T> dataset)
        {
            throw new NotImplementedException();
        }
    }


}
