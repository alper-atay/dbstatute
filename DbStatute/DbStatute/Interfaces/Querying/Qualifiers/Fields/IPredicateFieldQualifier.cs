﻿using Basiclog;
using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;

namespace DbStatute.Interfaces.Querying.Qualifiers.Fields
{
    public interface IPredicateFieldQualifier : ISettableSpecializedField<ReadOnlyLogbookPredicate<object>>, IFieldPredicateMap
    {
    }

    public interface IPredicateFieldQualifier<TModel> : ISettableSpecializedField<TModel, ReadOnlyLogbookPredicate<object>>, IPredicateFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}