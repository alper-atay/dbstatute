using Basiclog;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying
{
    public interface IQueryQualifier
    {
        IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap { get; }
        IReadOnlyDictionary<string, object> ValueMap { get; }

        IReadOnlyLogbook BuildQueryGroup(out QueryGroup queryGroup);

        IFieldQualifier GetFieldQualifier();
    }

    public interface IQueryQualifier<TId, TModel> : IQueryQualifier
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        new IFieldQualifier<TId, TModel> GetFieldQualifier();

        bool SetPredicate(Expression<Func<TModel, object>> property, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false);

        bool SetPredicate(string propertyName, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false);

        bool SetValue(Expression<Func<TModel, object>> property, object value);

        bool SetValue(string propertyName, object value);

        bool UnsetPredicate(Expression<Func<TModel, object>> property);

        bool UnsetPredicate(string propertyName);

        bool UnsetValue(Expression<Func<TModel, object>> property);

        bool UnsetValue(string propertyName, object value);
    }
}