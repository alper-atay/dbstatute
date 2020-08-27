using Basiclog;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IQueryQualifier : IReadOnlyLogbookTestable
    {
        IFieldQualifier FieldQualifier { get; }
        IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap { get; }
        IReadOnlyDictionary<string, object> ValueMap { get; }

        IReadOnlyLogbook GetQueryGroup(out QueryGroup queryGroup);

        protected static IReadOnlyLogbook GetQueryGroup(IQueryQualifier @this, out QueryGroup queryGroup)
        {
            queryGroup = null;
            ILogbook logs = Logger.NewLogbook();

            IFieldQualifier fieldQualifier = @this.FieldQualifier;
            IEnumerable<Field> fields = fieldQualifier.Fields;

            ICollection<QueryField> queryFields = new Collection<QueryField>();

            foreach (Field field in fields)
            {
                string name = field.Name;
                bool valueFound = @this.ValueMap.TryGetValue(name, out object value);
                bool predicateFound = @this.PredicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate);

                if (valueFound)
                {
                    QueryField queryField = new QueryField(field, value);
                    queryFields.Add(queryField);
                }

                if (valueFound && predicateFound)
                {
                    logs.AddRange(predicate.Invoke(value));
                }
            }

            int queryFieldCount = queryFields.Count;

            if (queryFieldCount > 0)
            {
                queryGroup = new QueryGroup(queryFields);
            }

            return logs;
        }

        protected static IReadOnlyLogbook Test(IQueryQualifier @this)
        {
            ILogbook logs = Logger.NewLogbook();

            IFieldQualifier fieldQualifier = @this.FieldQualifier;
            IEnumerable<Field> fields = fieldQualifier.Fields;

            foreach (Field field in fields)
            {
                string name = field.Name;
                bool valueFound = @this.ValueMap.TryGetValue(name, out object value);
                bool predicateFound = @this.PredicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate);

                if (valueFound && predicateFound)
                {
                    logs.AddRange(predicate.Invoke(value));
                }
            }

            return logs;
        }
    }

    public interface IQueryQualifier<TModel> : IQueryQualifier
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        ReadOnlyLogbookPredicate<object> GetPredicateOrDefault(Expression<Func<TModel, object>> expression);

        ReadOnlyLogbookPredicate<object> GetPredicateOrDefault(string name);

        object GetValueOrDefault(Expression<Func<TModel, object>> expression);

        object GetValueOrDefault(string name);

        bool SetPredicate(Expression<Func<TModel, object>> expression, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false);

        bool SetPredicate(string name, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false);

        bool SetValue(Expression<Func<TModel, object>> expression, object value);

        bool SetValue(string name, object value);

        bool UnsetPredicate(Expression<Func<TModel, object>> expression);

        bool UnsetPredicate(string name);

        bool UnsetValue(Expression<Func<TModel, object>> expression);

        bool UnsetValue(string name, object value);
    }
}