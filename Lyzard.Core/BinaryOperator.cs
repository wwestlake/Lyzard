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
