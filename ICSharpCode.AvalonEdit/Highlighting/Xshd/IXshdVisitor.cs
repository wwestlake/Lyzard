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

namespace ICSharpCode.AvalonEdit.Highlighting.Xshd
{
	/// <summary>
	/// A visitor over the XSHD element tree.
	/// </summary>
	public interface IXshdVisitor
	{
		/// <summary/>
		object VisitRuleSet(XshdRuleSet ruleSet);
		
		/// <summary/>
		object VisitColor(XshdColor color);
		
		/// <summary/>
		object VisitKeywords(XshdKeywords keywords);
		
		/// <summary/>
		object VisitSpan(XshdSpan span);
		
		/// <summary/>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "A VB programmer implementing a visitor?")]
		object VisitImport(XshdImport import);
		
		/// <summary/>
		object VisitRule(XshdRule rule);
	}
}
