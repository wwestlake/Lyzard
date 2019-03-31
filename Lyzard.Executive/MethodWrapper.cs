using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Executive
{
    public class MethodWrapper
    {
        private object _target;
        private MethodInfo _methodInfo;

        public MethodWrapper(object target, string methodName)
        {
            _target = target;
            _methodInfo = target.GetType().GetMethod(methodName);
            var a = 1;
        }

        public object Invoke(params object[] args)
        {
            return _methodInfo.Invoke(_target, args);
        }
    }
}
