/* naudio/NAudio.WaveFormRenderer is licensed under the
 * MIT License
 * https://github.com/naudio/NAudio.WaveFormRenderer/blob/master/LICENSE
 */

namespace WaveFormRendererLib
{
    public class PeakInfo
    {
        public PeakInfo(float min, float max)
        {
            Max = max;
            Min = min;
        }

        public float Min { get; private set; }
        public float Max { get; private set; }
    }
}