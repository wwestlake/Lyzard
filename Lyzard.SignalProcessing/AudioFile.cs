using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.SignalProcessing
{
    public class AudioFile : IDisposable
    {
        private AudioFileReader _reader;
        int _channels;
        int _sampleRate;
        float[] _channel1;
        float[] _channel2;
        int _length;

        public AudioFile(string filepath)
        {
            _reader = new AudioFileReader(filepath);
            _sampleRate = _reader.ToWaveProvider().WaveFormat.SampleRate;
            _channels = _reader.ToWaveProvider().WaveFormat.Channels;
            float[] data = new float[_reader.Length];
            _reader.Read(data, 0, data.Length);
            if (_channels == 1)
            {
                _channel1 = data;
            } else
            {
                _channel1 = new float[data.Length / 2];
                _channel2 = new float[data.Length / 2];
                var chidx = 0;
                for(int i = 0; i < data.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        _channel1[chidx] = data[i];
                    } else
                    {
                        _channel2[chidx] = data[i];
                        chidx++;
                    }
                }
            }
            _length =_channel1.Length;
        }

        public float[] Channel1 { get { return _channel1; } }
        public float[] Channel2 { get { return _channel2; } }
        public int Channels { get { return _channels; } }
        public int SampleRate { get { return _sampleRate; } }
        public int Length { get { return _length; } }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
