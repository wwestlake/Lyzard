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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Xml
{
	/// <summary> Information about syntax error that occured during parsing </summary>
	public class SyntaxError: TextSegment
	{
		/// <summary> Object for which the error occured </summary>
		public AXmlObject Object { get; internal set; }
		/// <summary> Textual description of the error </summary>
		public string Message { get; internal set; }
		/// <summary> Any user data </summary>
		public object Tag { get; set; }
		
		internal SyntaxError Clone(AXmlObject newOwner)
		{
			return new SyntaxError {
				Object = newOwner,
				Message = Message,
				Tag = Tag,
				StartOffset = StartOffset,
				EndOffset = EndOffset,
			};
		}
	}
}
