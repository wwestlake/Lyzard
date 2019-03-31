using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Executive
{
    public class PropertyWrapper
    {
        PropertyInfo _propertyInfo;
        private object _target;
        private MethodInfo _getterInfo;
        private MethodInfo _setterInfo;

        public PropertyWrapper(object target, string property)
        {
            _target = target;
            _propertyInfo = target.GetType().GetProperty(property);

            _getterInfo = _propertyInfo.GetGetMethod();
            _setterInfo = _propertyInfo.GetSetMethod();
        }

        public object Get()
        {
            return _getterInfo.Invoke(_target, new object[] { });
        }

        public void Set(object value)
        {
            _setterInfo.Invoke(_target, new object[] { value });
        }
        

    }
}
