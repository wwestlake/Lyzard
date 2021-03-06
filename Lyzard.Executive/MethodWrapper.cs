﻿/* 
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
using System.Reflection;

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
