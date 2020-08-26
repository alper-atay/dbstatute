using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace DbStatute.Querying
{
    public class QueryQualifier<TId, TModel> : IQueryQualifier<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly FieldQualifier<TId, TModel> _fieldQualifier = new FieldQualifier<TId, TModel>();
        private readonly Dictionary<string, ReadOnlyLogbookPredicate<object>> _predicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
        private readonly Dictionary<string, object> _valueMap = new Dictionary<string, object>();

        public IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap => _predicateMap;
        public IReadOnlyDictionary<string, object> ValueMap => _valueMap;

        public virtual IReadOnlyLogbook BuildQueryGroup(out QueryGroup queryGroup)
        {
            queryGroup = null;

            ILogbook logs = Logger.NewLogbook();

            IFieldQualifier fieldQualifier = GetFieldQualifier();
            IEnumerable<Field> fields = fieldQualifier.Fields;

            ICollection<QueryField> queryFields = new Collection<QueryField>();

            foreach (Field field in fields)
            {
                string name = field.Name;
                bool valueFound = ValueMap.TryGetValue(name, out object value);
                bool predicateFound = PredicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate);

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

        public IFieldQualifier<TId, TModel> GetFieldQualifier()
        {
            return _fieldQualifier;
        }

        IFieldQualifier IQueryQualifier.GetFieldQualifier()
        {
            return _fieldQualifier;
        }

        public bool SetPredicate(Expression<Func<TModel, object>> property, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            if (_predicateMap.ContainsKey(propertyName))
            {
                if (overrideEnabled)
                {
                    _predicateMap.Remove(propertyName);
                }
                else
                {
                    throw new InvalidOperationException($"Already registered a predicate for {propertyName} named property");
                }
            }

            return _predicateMap.TryAdd(propertyName, predicate);
        }

        public bool SetPredicate(string propertyName, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or white space", nameof(propertyName));
            }

            if (_predicateMap.ContainsKey(propertyName))
            {
                if (overrideEnabled)
                {
                    _predicateMap.Remove(propertyName);
                }
                else
                {
                    throw new InvalidOperationException($"Already registered a predicate for {propertyName} named property");
                }
            }

            return _predicateMap.TryAdd(propertyName, predicate);
        }

        public bool SetValue(Expression<Func<TModel, object>> property, object value)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            _fieldQualifier.SetField(property);

            return _valueMap.TryAdd(propertyName, value);
        }

        public bool SetValue(string propertyName, object value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or white space", nameof(propertyName));
            }

            _fieldQualifier.SetField(propertyName);

            return _valueMap.TryAdd(propertyName, value);
        }

        public bool UnsetPredicate(Expression<Func<TModel, object>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _predicateMap.Remove(propertyName);
        }

        public bool UnsetPredicate(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or white space", nameof(propertyName));
            }

            return _predicateMap.Remove(propertyName);
        }

        public bool UnsetValue(Expression<Func<TModel, object>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            _fieldQualifier.SetField(property);

            return _valueMap.Remove(propertyName);
        }

        public bool UnsetValue(string propertyName, object value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or white space", nameof(propertyName));
            }

            _fieldQualifier.SetField(propertyName);

            return _valueMap.Remove(propertyName);
        }
    }
}