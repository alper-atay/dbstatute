using System;

namespace DbStatute.Interfaces
{
    public interface ISingleSelectById<TId, TModel> : ISingleSelect<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        TId Id { get; }
    }
}