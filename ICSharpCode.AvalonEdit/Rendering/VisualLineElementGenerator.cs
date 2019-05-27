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

namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// Abstract base class for generators that produce new visual line elements.
	/// </summary>
	public abstract class VisualLineElementGenerator
	{
		/// <summary>
		/// Gets the text run construction context.
		/// </summary>
		protected ITextRunConstructionContext CurrentContext { get; private set; }
		
		/// <summary>
		/// Initializes the generator for the <see cref="ITextRunConstructionContext"/>
		/// </summary>
		public virtual void StartGeneration(ITextRunConstructionContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			this.CurrentContext = context;
		}
		
		/// <summary>
		/// De-initializes the generator.
		/// </summary>
		public virtual void FinishGeneration()
		{
			this.CurrentContext = null;
		}
		
		/// <summary>
		/// Should only be used by VisualLine.ConstructVisualElements.
		/// </summary>
		internal int cachedInterest;
		
		/// <summary>
		/// Gets the first offset >= startOffset where the generator wants to construct an element.
		/// Return -1 to signal no interest.
		/// </summary>
		public abstract int GetFirstInterestedOffset(int startOffset);
		
		/// <summary>
		/// Constructs an element at the specified offset.
		/// May return null if no element should be constructed.
		/// </summary>
		/// <remarks>
		/// Avoid signalling interest and then building no element by returning null - doing so
		/// causes the generated <see cref="VisualLineText"/> elements to be unnecessarily split
		/// at the position where you signalled interest.
		/// </remarks>
		public abstract VisualLineElement ConstructElement(int offset);
	}
	
	internal interface IBuiltinElementGenerator
	{
		void FetchOptions(TextEditorOptions options);
	}
}
