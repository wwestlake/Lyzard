using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.Folding
{
    public class RegionFoldingStrategy : AbstractFoldingStrategy
    {

        public string OpenRegion { get; set; }
        public string CloseRegion { get; set; }

        public RegionFoldingStrategy()
        {
            OpenRegion = "#region";
            CloseRegion = "#endregion";
        }

        public override IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;
            return CreateNewFoldings(document);
        }

        public IEnumerable<NewFolding> CreateNewFoldings(ITextSource document)
        {
            List<NewFolding> newFoldings = new List<NewFolding>();

            Stack<int> startOffsets = new Stack<int>();
            int lastNewLineOffset = 0;
            string openingBrace = OpenRegion;
            string closingBrace = CloseRegion;
            for (int i = 0; i < document.TextLength; i++)
            {
                char c = document.GetCharAt(i);
                if (document.Text.Length < CloseRegion.Length) return newFoldings;
                if (i + CloseRegion.Length + 1 > document.Text.Length) return newFoldings;

                string s = document.Text.Substring(i, CloseRegion.Length + 1);
                if (s.StartsWith(OpenRegion))
                {
                    startOffsets.Push(i);
                }
                else if (s.StartsWith(CloseRegion) && startOffsets.Count > 0)
                {
                    int startOffset = startOffsets.Pop();
                    // don't fold if opening and closing brace are on the same line
                    if (startOffset < lastNewLineOffset)
                    {
                        newFoldings.Add(new NewFolding(startOffset, i + 1));
                    }
                }
                else if (c == '\n' || c == '\r')
                {
                    lastNewLineOffset = i + 1;
                }
            }
            newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return newFoldings;

        }

        public override IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset, IEnumerable<NewFolding> existingFoldings)
        {
            var newFoldings = CreateNewFoldings(document, out firstErrorOffset);
            var list = new List<NewFolding>(newFoldings);
            list.AddRange(existingFoldings);
            list.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return list;
        }
    }
}
