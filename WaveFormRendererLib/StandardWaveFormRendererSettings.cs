/* naudio/NAudio.WaveFormRenderer is licensed under the
 * MIT License
 * https://github.com/naudio/NAudio.WaveFormRenderer/blob/master/LICENSE
 */

using System.Drawing;

namespace WaveFormRendererLib
{
    public class StandardWaveFormRendererSettings : WaveFormRendererSettings
    {
        public StandardWaveFormRendererSettings()
        {
            PixelsPerPeak = 1;
            SpacerPixels = 0;
            TopPeakPen = Pens.Maroon;
            BottomPeakPen = Pens.Peru;
        }


        public override Pen TopPeakPen { get; set; }

        // not needed
        public override Pen TopSpacerPen { get; set; }
        
        public override Pen BottomPeakPen { get; set; }
        
        // not needed
        public override Pen BottomSpacerPen { get; set; }
    }
}