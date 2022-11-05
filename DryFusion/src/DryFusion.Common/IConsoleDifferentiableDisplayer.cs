using System;
using System.Threading.Tasks;
using DryFusion.Common.Models;

namespace DryFusion.Common
{
    public interface IConsoleDifferentiableDisplayer<T>
    {
        Task DisplayMinDifferential<T2>(Func<T2, string> successMessage, string failureMessage, T2 defaultResult) 
            where T2 : class, IDifferentiable<T>;
    }
}