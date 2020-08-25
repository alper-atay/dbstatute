using System;
using System.Collections.Generic;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelectById<TId, TModel> : IMultipleSelect<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        IEnumerable<TId> Ids { get; }
        ICacheable Cacheable { get; set; }
    }
}