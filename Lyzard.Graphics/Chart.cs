using System.Drawing;

namespace Lyzard.GraphicsLib
{
    public abstract class Chart
    {
        private GraphDescription _description;

        public Chart(GraphDescription description)
        {
            _description = description;
        }


        public Bitmap Render<T>(DataSet<T> dataset)
        {
            Bitmap bmp = new Bitmap(_description.Width, _description.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                return RenderChart(g, dataset);
            }
        }

        protected abstract Bitmap RenderChart<T>(Graphics g, DataSet<T> dataset);

    }
}
