using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Common.Models
{
    public abstract class IntDifferentiable : IDifferentiable<int>
    {
        public int AbsoluteDifference { get; }

        public IntDifferentiable(int value1, int value2)
        {
            AbsoluteDifference = Math.Abs(value1 - value2);
        }
    }
}
