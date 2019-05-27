/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WaveFormRendererLib
{
    public class WaveFormRendererSettings
    {
        protected WaveFormRendererSettings()
        {
            Width = 800;
            TopHeight = 50;
            BottomHeight = 50;
            PixelsPerPeak = 1;
            SpacerPixels = 0;
            BackgroundColor = Color.Beige;
        }

        // for display purposes only
        public string Name { get; set; }

        public int Width { get; set; }

        public int TopHeight { get; set; }
        public int BottomHeight { get; set; }
        public int PixelsPerPeak { get; set; }
        public int SpacerPixels { get; set; }
        public virtual Pen TopPeakPen { get; set; }
        public virtual Pen TopSpacerPen { get; set; }
        public virtual Pen BottomPeakPen { get; set; }
        public virtual Pen BottomSpacerPen { get; set; }
        public bool DecibelScale { get; set; }
        public Color BackgroundColor { get; set; }
        public Image BackgroundImage { get; set; }
        public Brush BackgroundBrush {
            get
            {
                if (BackgroundImage == null) return new SolidBrush(BackgroundColor);
                return new TextureBrush(BackgroundImage,WrapMode.Clamp);
            }
        }

        protected static Pen CreateGradientPen(int height, Color startColor, Color endColor)
        {
            var brush = new LinearGradientBrush(new Point(0, 0), new Point(0, height), startColor, endColor);
            return new Pen(brush);
        }
    }
}