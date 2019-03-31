using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Interfaces
{
    public interface IPlugin
   {
        string Name { get; }
        void Initialize();
        void StartPlugin();


    }
}
