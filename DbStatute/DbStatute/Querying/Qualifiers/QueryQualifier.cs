using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers
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

        public IReadOnlyLogbook Test()
        {
            return IQueryQualifier.Test(this);
        }
    }

    public class QueryQualifier<TModel> : IQueryQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly FieldQualifier<TModel> _fieldQualifier;
        private readonly Dictionary<string, ReadOnlyLogbookPredicate<object>> _predicateMap;
        private readonly Dictionary<string, object> _valueMap;

        public QueryQualifier()
        {
            _fieldQualifier = new FieldQualifier<TModel>();
            _predicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
            _valueMap = new Dictionary<string, object>();
        }

        public QueryQualifier(FieldQualifier<TModel> fieldQualifier)
        {
            _fieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            _predicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
            _valueMap = new Dictionary<string, object>();
        }

        public IFieldQualifier<TModel> FieldQualifier => _fieldQualifier;
        IFieldQualifier IQueryQualifier.FieldQualifier => _fieldQualifier;
        public IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap => _predicateMap;
        public IReadOnlyDictionary<string, object> ValueMap => _valueMap;

        public ReadOnlyLogbookPredicate<object> GetPredicateOrDefault(Expression<Func<TModel, object>> expression)
        {
            string name = expression.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _predicateMap.GetValueOrDefault(name);
        }

        public ReadOnlyLogbookPredicate<object> GetPredicateOrDefault(string name)
        {
            return _predicateMap.GetValueOrDefault(name);
        }

        public virtual IReadOnlyLogbook GetQueryGroup(out QueryGroup queryGroup)
        {
            return IQueryQualifier.GetQueryGroup(this, out queryGroup);
        }

        public object GetValueOrDefault(Expression<Func<TModel, object>> expression)
        {
            string name = expression.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _valueMap.GetValueOrDefault(name);
        }

        public object GetValueOrDefault(string name)
        {
            return _valueMap.GetValueOrDefault(name);
        }

        public bool SetPredicate(Expression<Func<TModel, object>> expression, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false)
        {
            string name = expression.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            if (_predicateMap.ContainsKey(name))
            {
                if (overrideEnabled)
                {
                    _predicateMap.Remove(name);
                }
                else
                {
                    throw new InvalidOperationException($"Already registered a predicate for {name} named property");
                }
            }

            return _predicateMap.TryAdd(name, predicate);
        }

        public bool SetPredicate(string name, ReadOnlyLogbookPredicate<object> expression, bool overrideEnabled = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or white space", nameof(name));
            }

            if (_predicateMap.ContainsKey(name))
            {
                if (overrideEnabled)
                {
                    _predicateMap.Remove(name);
                }
                else
                {
                    throw new InvalidOperationException($"Already registered a predicate for {name} named property");
                }
            }

            return _predicateMap.TryAdd(name, expression);
        }

        public bool SetValue(Expression<Func<TModel, object>> expression, object value)
        {
            string name = expression.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            _fieldQualifier.SetField(expression);

            return _valueMap.TryAdd(name, value);
        }

        public bool SetValue(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or white space", nameof(name));
            }

            _fieldQualifier.SetField(name);

            return _valueMap.TryAdd(name, value);
        }

        public IReadOnlyLogbook Test()
        {
            return IQueryQualifier.Test(this);
        }

        public bool UnsetPredicate(Expression<Func<TModel, object>> expression)
        {
            string name = expression.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _predicateMap.Remove(name);
        }

        public bool UnsetPredicate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or white space", nameof(name));
            }

            return _predicateMap.Remove(name);
        }

        public bool UnsetValue(Expression<Func<TModel, object>> expression)
        {
            string propertyName = expression.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            _fieldQualifier.SetField(expression);

            return _valueMap.Remove(propertyName);
        }

        public bool UnsetValue(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or white space", nameof(name));
            }

            _fieldQualifier.SetField(name);

            return _valueMap.Remove(name);
        }
    }
}