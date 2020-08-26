using System;

namespace DbStatute.Interfaces
{
    public interface IMultipleMerge<TId, TModel> : IMerge
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
    }
}