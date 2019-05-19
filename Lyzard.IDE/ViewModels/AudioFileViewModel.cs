using Lyzard.SignalProcessing;
using NAudio.Wave;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WaveFormRendererLib;

namespace Lyzard.IDE.ViewModels
{
    public class AudioFileViewModel : DocumentViewModelBase
    {
        private PlotModel _plotModel;
        private AudioFile _audioFile;
        private string _filepath;

        public AudioFileViewModel(string filepath)
        {
            FilePath = filepath;

            var blockSize = 200;
            var sampleInterval = 200;
            var scaleFactor = 4;

            var maxPeakProvider = new MaxPeakProvider();
            var rmsPeakProvider = new RmsPeakProvider(blockSize); // e.g. 200
            var samplingPeakProvider = new SamplingPeakProvider(sampleInterval); // e.g. 200
            var averagePeakProvider = new AveragePeakProvider(scaleFactor); // e.g. 4

            var myRendererSettings = new StandardWaveFormRendererSettings();
            myRendererSettings.Width = 640;
            myRendererSettings.TopHeight = 32;
            myRendererSettings.BottomHeight = 32;

            var renderer = new WaveFormRenderer();
            Image = ConvertImage(renderer.Render(FilePath, averagePeakProvider, myRendererSettings));
        }

        public BitmapImage Image { get; set; }

        private BitmapImage ConvertImage(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private bool playing = false;

        public ICommand Play => new DelegateCommand((x) => {
            playing = true;
            OnPropertyChanged();
            Task.Factory.StartNew(() => {
                using (var audioFile = new AudioFileReader(FilePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(1000);
                    }
                }
            });
        },(x) => !playing);

        private void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            playing = false;
            OnPropertyChanged("Play");
        }

        public string FilePath { get { return _filepath; } set { _filepath = value; OnPropertyChanged(); } }
        public override bool CanSave(object param)
        {
            return false;
        }

        public override void Close()
        {
            DockManagerViewModel.DocumentManager.Documents.Remove(this);
        }

        public override void Save(object param)
        {
            
        }

        public override void SaveAs(object param)
        {
            
        }
    }
}
