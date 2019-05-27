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

namespace ICSharpCode.AvalonEdit.Utils
{
	/// <summary>
	/// Reuse the same instances for boxed booleans.
	/// </summary>
	static class Boxes
	{
		public static readonly object True = true;
		public static readonly object False = false;
		
		public static object Box(bool value)
		{
			return value ? True : False;
		}
	}
}
