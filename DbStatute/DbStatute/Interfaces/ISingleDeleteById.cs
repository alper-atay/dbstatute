using System;

namespace DbStatute.Interfaces
{
    public interface ISingleDeleteById<TId, TModel, TSingleSelectById> : ISingleDelete<TId, TModel, TSingleSelectById>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSingleSelectById : ISingleSelectById<TId, TModel>
    {
        TSingleSelectById SingleSelectById { get; }
    }
}