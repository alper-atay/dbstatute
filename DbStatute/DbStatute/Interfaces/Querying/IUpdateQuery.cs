using Basiclog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying
{
    public interface IUpdateQuery : IFieldQualifier, IReadOnlyLogbookTestable
    {
        IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap { get; }

        IReadOnlyDictionary<string, object> ValueMap { get; }
    }

    public interface IUpdateQuery<TId, TModel> : IUpdateQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        TModel UpdaterModel { get; }

        bool IsFieldEnabled<TValue>(Expression<Func<TModel, TValue>> property);

        void RegisterPredicate<TValue>(Expression<Func<TModel, TValue>> property, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false);

        void SetField<TValue>(Expression<Func<TModel, TValue>> property, TValue value);

        bool UnregisterPredicate<TValue>(Expression<Func<TModel, TValue>> property);

        bool UnsetField<TValue>(Expression<Func<TModel, TValue>> property);
    }
}