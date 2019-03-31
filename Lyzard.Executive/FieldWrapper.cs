using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Executive
{
    public class FieldWrapper
    {
        private object _target;
        private FieldInfo _fieldInfo;

        public FieldWrapper(object target, string member)
        {
            _target = target;
            _fieldInfo = _target.GetType().GetField(member, BindingFlags.Public | BindingFlags.Instance);
        }

        public object Value
        {
            get
            {
                return _fieldInfo.GetValue(_target);
            }
            set
            {
                _fieldInfo.SetValue(_target, value);
            }
        }

        

    }
}
