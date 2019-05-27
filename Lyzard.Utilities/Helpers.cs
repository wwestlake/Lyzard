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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Utilities
{
    public static class StringHelpers
    {
        public static string LeftOf(this string src, char chr)
        {
            return src.Substring(0, src.IndexOf(chr));
        }

        public static string MakeRelativePath(this string path1, string path2)
        {
            return path2.Substring(path1.Length).TrimStart(Path.DirectorySeparatorChar);
        }

    }
    
    public static class Helpers
    {
        public static void PrintLoadedAssemblies()
        {
            Assembly[] assys = AppDomain.CurrentDomain.GetAssemblies();
            Console.WriteLine("----------------------------------");
    
            foreach (Assembly assy in assys)
            {
                Console.WriteLine(assy.FullName.LeftOf(','));
            }
    
            Console.WriteLine("----------------------------------");
        }
    }
}
