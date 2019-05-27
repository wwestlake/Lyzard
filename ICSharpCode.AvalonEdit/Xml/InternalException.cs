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

namespace ICSharpCode.AvalonEdit.Xml
{
	/// <summary>
	/// Exception used for internal errors in XML parser.
	/// This exception indicates a bug in AvalonEdit.
	/// </summary>
	[Serializable()]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic", Justification = "This exception is not public because it is not supposed to be caught by user code - it indicates a bug in AvalonEdit.")]
	class InternalException : Exception
	{
		/// <summary>
		/// Creates a new InternalException instance.
		/// </summary>
		public InternalException() : base()
		{
		}
		
		/// <summary>
		/// Creates a new InternalException instance.
		/// </summary>
		public InternalException(string message) : base(message)
		{
		}
		
		/// <summary>
		/// Creates a new InternalException instance.
		/// </summary>
		public InternalException(string message, Exception innerException) : base(message, innerException)
		{
		}
		
		/// <summary>
		/// Creates a new InternalException instance.
		/// </summary>
		protected InternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
