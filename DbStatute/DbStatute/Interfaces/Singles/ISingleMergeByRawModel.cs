﻿using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMergeByRawModel : ISingleMergeBase, ISourceModel
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface ISingleMergeByRawModel<TModel> : ISingleMergeBase<TModel>, ISourceModel<TModel>, ISingleMergeByRawModel
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}