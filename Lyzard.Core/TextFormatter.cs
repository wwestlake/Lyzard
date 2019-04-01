using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Core
{
    public class TextFormatter : BaseBlock
    {
        public TextFormatter()
        {
            AddInput<string>("Format");
            AddInput("Arguments", new object[] { });
            AddOutput("FormatedText", () => FormatText());
        }

        private string FormatText()
        {
            var format = GetValue("Format") as string;
            var args = GetValue("Arguments") as object[];
            return string.Format(format, args);
        }



    }
}
