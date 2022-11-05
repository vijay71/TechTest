using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Common.Models
{
    /// <summary>
    /// A class implementing this interface contains
    /// data that is differentiable (meaning there is
    /// some kind of numerical difference that can be
    /// calculated from data within the class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDifferentiable<T>
    {
        T AbsoluteDifference { get; }
    }
}
