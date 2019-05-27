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

namespace WaveFormRendererLib
{
    public class SamplingPeakProvider : PeakProvider
    {
        private readonly int sampleInterval;

        public SamplingPeakProvider(int sampleInterval) 
        {
            this.sampleInterval = sampleInterval;
        }

        public override PeakInfo GetNextPeak()
        {
            var samplesRead = Provider.Read(ReadBuffer,0,ReadBuffer.Length);
            var max = 0.0f;
            var min = 0.0f;
            for (int x = 0; x < samplesRead; x += sampleInterval)
            {
                max = Math.Max(max, ReadBuffer[x]);
                min = Math.Min(min, ReadBuffer[x]);
            }

            return new PeakInfo(min,max);
        }
    }
}