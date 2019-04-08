using Lyzard.IDE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.Messages
{
    public class FileSavedMessage
    {
        public DocumentViewModelBase Vm { get; set; }
    }
}
