using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Executive
{
    public class ClassInstanceWrapper
    {

        public ClassInstanceWrapper(Assembly assembly, string fullClassName)
        {
            var type = assembly.GetTypes().Where((t) => t.FullName == fullClassName).FirstOrDefault();
            if (type == null) throw new TypeNotFoundException(fullClassName, $"class not found in assembly '{assembly.FullName}'");
            Instance = assembly.CreateInstance(type.FullName);
        }


        public object Instance { get; private set; }


    }
}
