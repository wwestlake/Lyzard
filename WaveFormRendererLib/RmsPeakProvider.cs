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
    public class RmsPeakProvider : PeakProvider
    {
        private readonly int blockSize;

        public RmsPeakProvider(int blockSize)
        {
            this.blockSize = blockSize;
        }

        public override PeakInfo GetNextPeak()
        {
            var samplesRead = Provider.Read(ReadBuffer, 0, ReadBuffer.Length);

            var max = 0.0f;
            for (int x = 0; x < samplesRead; x += blockSize)
            {
                double total = 0.0;
                for (int y = 0; y < blockSize && x + y < samplesRead; y++)
                {
                    total += ReadBuffer[x + y] * ReadBuffer[x + y];
                }
                var rms = (float) Math.Sqrt(total/blockSize);

                max = Math.Max(max, rms);
            }

            return new PeakInfo(0 -max, max);
        }
    }
}