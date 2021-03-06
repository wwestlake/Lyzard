﻿/* 
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

namespace ICSharpCode.AvalonEdit.Highlighting.Xshd
{
	/// <summary>
	/// An element in a XSHD rule set.
	/// </summary>
	[Serializable]
	public abstract class XshdElement
	{
		/// <summary>
		/// Gets the line number in the .xshd file.
		/// </summary>
		public int LineNumber { get; set; }
		
		/// <summary>
		/// Gets the column number in the .xshd file.
		/// </summary>
		public int ColumnNumber { get; set; }
		
		/// <summary>
		/// Applies the visitor to this element.
		/// </summary>
		public abstract object AcceptVisitor(IXshdVisitor visitor);
	}
}
