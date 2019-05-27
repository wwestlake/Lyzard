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

namespace WaveFormRendererLib
{
    public class SoundCloudBlockWaveFormSettings : WaveFormRendererSettings
    {
        private readonly Color topSpacerStartColor;
        private Pen topPen;
        private Pen topSpacerPen;
        private Pen bottomPen;
        private Pen bottomSpacerPen;

        private int lastTopHeight;
        private int lastBottomHeight;

        public SoundCloudBlockWaveFormSettings(Color topPeakColor, Color topSpacerStartColor, Color bottomPeakColor, Color bottomSpacerColor)
        {
            this.topSpacerStartColor = topSpacerStartColor;
            topPen = new Pen(topPeakColor);
            bottomPen = new Pen(bottomPeakColor);
            bottomSpacerPen = new Pen(bottomSpacerColor);
            PixelsPerPeak = 4;
            SpacerPixels = 2;
            BackgroundColor = Color.White;
            TopSpacerGradientStartColor = Color.White;
        }

        public override Pen TopPeakPen
        {
            get { return topPen; }
            set { topPen = value; }
        }

        public Color TopSpacerGradientStartColor { get; set; }

        public override Pen TopSpacerPen
        {
            get
            {
                if (topSpacerPen == null || lastBottomHeight != BottomHeight || lastTopHeight != TopHeight)
                {
                    topSpacerPen = CreateGradientPen(TopHeight, TopSpacerGradientStartColor, topSpacerStartColor);
                    lastBottomHeight = BottomHeight;
                    lastTopHeight = TopHeight;
                }
                return topSpacerPen;
            }
            set { topSpacerPen = value; }
        }


        public override Pen BottomPeakPen
        {
            get { return bottomPen; }
            set { bottomPen = value; }
        }


        public override Pen BottomSpacerPen
        {
            get { return bottomSpacerPen; }
            set { bottomSpacerPen = value; }
        }

    }
}