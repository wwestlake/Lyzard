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
        public BinaryOperator()
        {
            AddInput<T>("Left");
            AddInput<T>("Right");
            AddInput<Func<T,T,T>>("Operator");
            AddOutput<T>("Result", new OutputConnector<T>(() => {
                var left = (T)GetValue("Left");
                var right = (T)GetValue("Right");
                var op = (Func<T,T,T>)GetValue("Operator");
                return op(left, right);
            }));
        }
    }

    public class AddInteger : BinaryOperator<int>
    {
        public AddInteger() : base()
        {
            Input("Operator").Delegate = new InputConnector<Func<int, int, int>>(() => (l, r) => l + r);
        }
    }

}
