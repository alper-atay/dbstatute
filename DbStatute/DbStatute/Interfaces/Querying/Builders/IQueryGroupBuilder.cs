﻿using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IQueryGroupBuilder : IBuilder
    {
        IFieldQualifier FieldQualifier { get; }
        IPredicateFieldQualifier PredicateFieldQualifier { get; }
        IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public interface IQueryGroupBuilder<TModel> : IBuilder<QueryGroup>, IQueryGroupBuilder
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
    }
}