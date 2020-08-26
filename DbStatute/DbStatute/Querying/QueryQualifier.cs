using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Querying
{
    public class QueryQualifier : IQueryQualifier
    {
        public QueryQualifier()
        {
            PredicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
            ValueMap = new Dictionary<string, object>();
            FieldQualifier = IFieldQualifier.Empty;
        }

        public QueryQualifier(IFieldQualifier fieldQualifier)
        {
            PredicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
            ValueMap = new Dictionary<string, object>();
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public QueryQualifier(Dictionary<string, ReadOnlyLogbookPredicate<object>> predicateMap, Dictionary<string, object> valueMap)
        {
            PredicateMap = predicateMap ?? throw new ArgumentNullException(nameof(predicateMap));
            ValueMap = valueMap ?? throw new ArgumentNullException(nameof(valueMap));
            FieldQualifier = IFieldQualifier.Empty;
        }

        public QueryQualifier(Dictionary<string, ReadOnlyLogbookPredicate<object>> predicateMap, Dictionary<string, object> valueMap, IFieldQualifier fieldQualifier)
        {
            PredicateMap = predicateMap ?? throw new ArgumentNullException(nameof(predicateMap));
            ValueMap = valueMap ?? throw new ArgumentNullException(nameof(valueMap));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public IFieldQualifier FieldQualifier { get; }
        public IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap { get; }
        public IReadOnlyDictionary<string, object> ValueMap { get; }

        public virtual IReadOnlyLogbook GetQueryGroup(out QueryGroup queryGroup)
        {
            return IQueryQualifier.GetQueryGroup(this, out queryGroup);
        }
    }

    public class QueryQualifier<TModel> : IQueryQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly FieldQualifier<TModel> _fieldQualifier = new FieldQualifier<TModel>();
        private readonly Dictionary<string, ReadOnlyLogbookPredicate<object>> _predicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
        private readonly Dictionary<string, object> _valueMap = new Dictionary<string, object>();

        public IFieldQualifier<TModel> FieldQualifier => _fieldQualifier;
        IFieldQualifier IQueryQualifier.FieldQualifier => _fieldQualifier;
        public IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap => _predicateMap;
        public IReadOnlyDictionary<string, object> ValueMap => _valueMap;

        public virtual IReadOnlyLogbook GetQueryGroup(out QueryGroup queryGroup)
        {
            return IQueryQualifier.GetQueryGroup(this, out queryGroup);
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