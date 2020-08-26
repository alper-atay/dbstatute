using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Statutes;
using System;
using System.Collections.Generic;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelectByQueryAndFieldQualifier<TId, TModel, TSelectQuery, TFieldQualifier> : IMultipleSelectByQuery<TId, TModel, TSelectQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
        where TFieldQualifier : IFieldQualifier<TId, TModel>
    {
        int DynamicSelectedModelCount { get; }
        IEnumerable<dynamic> DynamicSelectedModels { get; }
        TFieldQualifier FieldQualifier { get; }
    }
}