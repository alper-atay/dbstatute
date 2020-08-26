using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute.Interfaces
{
    public interface ISingleMergeByFieldQualifier<TId, TModel, TFieldQualifier> : ISingleMerge<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TFieldQualifier : IFieldQualifier
    {
        TFieldQualifier FieldQualifier { get; }
    }
}