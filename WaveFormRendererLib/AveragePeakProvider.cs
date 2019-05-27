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
using System.Linq;

namespace WaveFormRendererLib
{
    public class AveragePeakProvider : PeakProvider
    {
        private readonly float scale;

        public AveragePeakProvider(float scale)
        {
            this.scale = scale;
        }

        public override PeakInfo GetNextPeak()
        {
            var samplesRead = Provider.Read(ReadBuffer, 0, ReadBuffer.Length);
            var sum = (samplesRead == 0) ? 0 : ReadBuffer.Take(samplesRead).Select(s => Math.Abs(s)).Sum();
            var average = sum/samplesRead;
            
            return new PeakInfo(average * (0 - scale), average * scale);
        }
    }
}