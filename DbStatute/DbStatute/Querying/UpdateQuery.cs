using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Internals;
using RepoDb;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace DbStatute.Querying
{
    public abstract class UpdateQuery<TId, TModel> : IUpdateQuery<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly Dictionary<string, ReadOnlyLogbookPredicate<object>> _predicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
        private readonly TModel _updaterModel = new TModel();
        private readonly Dictionary<string, object> _valueMap = new Dictionary<string, object>();
        public IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap => _predicateMap;
        public IEnumerable<Field> Fields => GetUpdateFields();
        public TModel UpdaterModel => (TModel)_updaterModel.Clone();
        public IReadOnlyDictionary<string, object> ValueMap => _valueMap;

        public bool IsFieldEnabled<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _valueMap.ContainsKey(propertyName);
        }

        public void RegisterPredicate<TValue>(Expression<Func<TModel, TValue>> property, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false)
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

            _predicateMap.Add(propertyName, predicate);
        }

        public void SetField<TValue>(Expression<Func<TModel, TValue>> property, TValue value)
        {
            Type valueType = typeof(TValue);
            if (!valueType.IsDbType())
            {
                throw new InvalidTypeException($"Value ({valueType.FullName}) is not resolvable to DbType");
            }

            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            Type modelType = typeof(TModel);
            PropertyInfo propertyInfo = modelType.GetProperty(propertyName);
            propertyInfo.SetValue(UpdaterModel, value);
        }

        public IReadOnlyLogbook Test()
        {
            ILogbook logs = Logger.New();

            foreach (KeyValuePair<string, ReadOnlyLogbookPredicate<object>> propertyValuePair in _predicateMap)
            {
                string name = propertyValuePair.Key;
                ReadOnlyLogbookPredicate<object> value = propertyValuePair.Value;

                if (_predicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate))
                {
                    logs.AddRange(predicate.Invoke(value));
                }
            }

            return logs;
        }

        public bool UnregisterPredicate<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            if (_predicateMap.ContainsKey(propertyName))
            {
                _predicateMap.Remove(propertyName);
                return true;
            }

            return false;
        }

        public bool UnsetField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _valueMap.Remove(propertyName);
        }

        private IEnumerable<Field> GetUpdateFields()
        {
            if (_valueMap.Count == 0)
            {
                return null;
            }

            ICollection<Field> fields = new Collection<Field>();

            foreach (string name in _predicateMap.Keys)
            {
                fields.Add(new Field(name));
            }

            return fields;
        }
    }
}