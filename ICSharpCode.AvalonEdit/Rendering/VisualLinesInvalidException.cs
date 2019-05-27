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
using System.Runtime.Serialization;

namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// A VisualLinesInvalidException indicates that you accessed the <see cref="TextView.VisualLines"/> property
	/// of the <see cref="TextView"/> while the visual lines were invalid.
	/// </summary>
	[Serializable]
	public class VisualLinesInvalidException  : Exception
	{
		/// <summary>
		/// Creates a new VisualLinesInvalidException instance.
		/// </summary>
		public VisualLinesInvalidException() : base()
		{
		}
		
		/// <summary>
		/// Creates a new VisualLinesInvalidException instance.
		/// </summary>
		public VisualLinesInvalidException(string message) : base(message)
		{
		}
		
		/// <summary>
		/// Creates a new VisualLinesInvalidException instance.
		/// </summary>
		public VisualLinesInvalidException(string message, Exception innerException) : base(message, innerException)
		{
		}
		
		/// <summary>
		/// Creates a new VisualLinesInvalidException instance.
		/// </summary>
		protected VisualLinesInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
