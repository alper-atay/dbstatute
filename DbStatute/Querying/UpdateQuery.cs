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
    public abstract class UpdateQuery<TId, TModel> : IUpdateQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly IDictionary<string, ReadOnlyLogbookPredicate<object>> _propertyPredicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
        private readonly IDictionary<string, object> _propertyValueMap = new Dictionary<string, object>();
        private readonly TModel _updaterModel = new TModel();
        public IEnumerable<Field> UpdateFields => GetUpdateFields();
        public TModel UpdaterModel => (TModel)_updaterModel.Clone();

        public bool IsFieldEnabled<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _propertyValueMap.ContainsKey(propertyName);
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

            foreach (KeyValuePair<string, ReadOnlyLogbookPredicate<object>> propertyValuePair in _propertyPredicateMap)
            {
                string name = propertyValuePair.Key;
                ReadOnlyLogbookPredicate<object> value = propertyValuePair.Value;

                if (_propertyPredicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate))
                {
                    logs.AddRange(predicate.Invoke(value));
                }
            }

            return logs;
        }

        public bool UnsetField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _propertyValueMap.Remove(propertyName);
        }

        protected void RegisterPredicate<TValue>(Expression<Func<TModel, TValue>> property, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            if (_propertyPredicateMap.ContainsKey(propertyName))
            {
                if (overrideEnabled)
                {
                    _propertyPredicateMap.Remove(propertyName);
                }
                else
                {
                    throw new InvalidOperationException($"Already registered a predicate for {propertyName} named property");
                }
            }

            _propertyPredicateMap.Add(propertyName, predicate);
        }

        protected bool UnregisterPredicate<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            if (_propertyPredicateMap.ContainsKey(propertyName))
            {
                _propertyPredicateMap.Remove(propertyName);
                return true;
            }

            return false;
        }

        private IEnumerable<Field> GetUpdateFields()
        {
            if (_propertyValueMap.Count == 0)
            {
                return null;
            }

            ICollection<Field> fields = new Collection<Field>();

            foreach (string name in _propertyPredicateMap.Keys)
            {
                fields.Add(new Field(name));
            }

            return fields;
        }
    }
}