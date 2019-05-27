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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Core
{
    public class BinaryOperator<T> : BaseBlock
    {

        /// <summary>
        /// Constructs a binary operator execution block
        /// that executes any binary operator on two input values
        /// the Left and Right operands
        /// </summary>
        public BinaryOperator(Func<T,T,T> oper)
        {
            AddInput<T>("Left");
            AddInput<T>("Right");
            AddInput<Func<T,T,T>>("Operator", oper);
            AddOutput<T>("Result", new OutputConnector<T>(() => {
                var left = (T)GetValue("Left");
                var right = (T)GetValue("Right");
                var op = (Func<T,T,T>)GetValue("Operator");
                return op(left, right);
            }));
        }
    }

    public class Add<T> : BinaryOperator<T>
    {
        public Add() : base((l, r) =>
            {
                dynamic a = l;
                dynamic b = r;
                return a + b;
            })
        { }
    }

    public class AddInteger : Add<int> { }
    public class AddFloat   : Add<float> { }
    public class AddDouble  : Add<double> { }
    
    public class Mul<T> : BinaryOperator<T>
    {
        public Mul() : base((l, r) =>
        {
            dynamic a = l;
            dynamic b = r;
            return a * b;
        })
        { }
    }

    public class MulInteger : Mul<int> { }
    public class MulFloat   : Mul<float> { }
    public class MulDouble  : Mul<double> { }

    public class Sub<T> : BinaryOperator<T>
    {
        public Sub() : base((l, r) =>
        {
            dynamic a = l;
            dynamic b = r;
            return a - b;
        })
        { }
    }

    public class SubInteger : Sub<int> { }
    public class SubFloat   : Sub<float> { }
    public class SubDouble  : Sub<double> { }

    public class Div<T> : BinaryOperator<T>
    {
        public Div() : base((l, r) =>
        {
            dynamic a = l;
            dynamic b = r;
            return a / b;
        })
        { }
    }

    public class DivInteger : Div<int> { }
    public class DivFloat   : Div<float> { }
    public class DivDouble  : Div<double> { }

}
