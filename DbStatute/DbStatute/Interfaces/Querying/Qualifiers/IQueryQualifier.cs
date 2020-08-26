using Basiclog;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IQueryQualifier
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
    }

    public interface IQueryQualifier<TModel> : IQueryQualifier
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

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