using DbStatute.Interfaces.Fundamentals.Multiples;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByRawModels<TModel> : IMultipleInsertBase<TModel>
        where TModel : class, IModel, new()
    {
        IEnumerable<TModel> RawModels { get; }
    }
}