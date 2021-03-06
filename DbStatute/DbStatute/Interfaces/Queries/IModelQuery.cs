﻿using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Queries
{
    public interface IModelQuery
    {
        IFieldQualifier Fields { get; }

        IPredicateFieldQualifier PredicateMap { get; }

        IValueFieldQualifier ValueMap { get; }
    }

    public interface IModelQuery<TModel> : IModelQuery
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> Fields { get; }

        new IPredicateFieldQualifier<TModel> PredicateMap { get; }

        new IValueFieldQualifier<TModel> ValueMap { get; }
    }
}