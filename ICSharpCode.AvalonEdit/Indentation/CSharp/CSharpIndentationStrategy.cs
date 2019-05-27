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
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Indentation.CSharp
{
	/// <summary>
	/// Smart indentation for C#.
	/// </summary>
	public class CSharpIndentationStrategy : DefaultIndentationStrategy
	{
		/// <summary>
		/// Creates a new CSharpIndentationStrategy.
		/// </summary>
		public CSharpIndentationStrategy()
		{
		}
		
		/// <summary>
		/// Creates a new CSharpIndentationStrategy and initializes the settings using the text editor options.
		/// </summary>
		public CSharpIndentationStrategy(TextEditorOptions options)
		{
			this.IndentationString = options.IndentationString;
		}
		
		string indentationString = "\t";
		
		/// <summary>
		/// Gets/Sets the indentation string.
		/// </summary>
		public string IndentationString {
			get { return indentationString; }
			set {
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException("Indentation string must not be null or empty");
				indentationString = value;
			}
		}
		
		/// <summary>
		/// Performs indentation using the specified document accessor.
		/// </summary>
		/// <param name="document">Object used for accessing the document line-by-line</param>
		/// <param name="keepEmptyLines">Specifies whether empty lines should be kept</param>
		public void Indent(IDocumentAccessor document, bool keepEmptyLines)
		{
			if (document == null)
				throw new ArgumentNullException("document");
			IndentationSettings settings = new IndentationSettings();
			settings.IndentString = this.IndentationString;
			settings.LeaveEmptyLines = keepEmptyLines;
			
			IndentationReformatter r = new IndentationReformatter();
			r.Reformat(document, settings);
		}
		
		/// <inheritdoc cref="IIndentationStrategy.IndentLine"/>
		public override void IndentLine(TextDocument document, DocumentLine line)
		{
			int lineNr = line.LineNumber;
			TextDocumentAccessor acc = new TextDocumentAccessor(document, lineNr, lineNr);
			Indent(acc, false);
			
			string t = acc.Text;
			if (t.Length == 0) {
				// use AutoIndentation for new lines in comments / verbatim strings.
				base.IndentLine(document, line);
			}
		}
		
		/// <inheritdoc cref="IIndentationStrategy.IndentLines"/>
		public override void IndentLines(TextDocument document, int beginLine, int endLine)
		{
			Indent(new TextDocumentAccessor(document, beginLine, endLine), true);
		}
	}
}
