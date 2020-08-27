using DbStatute.Interfaces.Fundamentals;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleSelectById : IMultipleSelectBase
    {
        IEnumerable<object> Ids { get; }
    }

    public interface IMultipleSelectById<TModel> : IMultipleSelectBase<TModel>, IMultipleSelectById
        where TModel : class, IModel, new()
    {
    }
}