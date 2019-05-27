/* naudio/NAudio.WaveFormRenderer is licensed under the
 * MIT License
 * https://github.com/naudio/NAudio.WaveFormRenderer/blob/master/LICENSE
 */

using NAudio.Wave;

namespace WaveFormRendererLib
{
    public interface IPeakProvider
    {
        void Init(ISampleProvider reader, int samplesPerPixel);
        PeakInfo GetNextPeak();
    }
}