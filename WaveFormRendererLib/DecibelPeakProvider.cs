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
using System;
using NAudio.Wave;

namespace WaveFormRendererLib
{
    class DecibelPeakProvider : IPeakProvider
    {
        private readonly IPeakProvider sourceProvider;
        private readonly double dynamicRange;

        public DecibelPeakProvider(IPeakProvider sourceProvider, double dynamicRange)
        {
            this.sourceProvider = sourceProvider;
            this.dynamicRange = dynamicRange;
        }

        public void Init(ISampleProvider reader, int samplesPerPixel)
        {
            throw new NotImplementedException();
        }

        public PeakInfo GetNextPeak()
        {
            var peak = sourceProvider.GetNextPeak();
            var decibelMax = 20 * Math.Log10(peak.Max);
            if (decibelMax < 0 - dynamicRange) decibelMax = 0 - dynamicRange;
            var linear = (float)((dynamicRange + decibelMax) / dynamicRange);
            return new PeakInfo(0 - linear, linear);
        }
    }
}