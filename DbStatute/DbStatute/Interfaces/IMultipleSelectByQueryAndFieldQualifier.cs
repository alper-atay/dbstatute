using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelectByQueryAndFieldQualifier<TId, TModel, TSelectQuery, TFieldQualifier> : IMultipleSelectByQuery<TId, TModel, TSelectQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
        where TFieldQualifier : IFieldQualifier<TId, TModel>
    {
        TFieldQualifier FieldQualifier { get; }
    }
}