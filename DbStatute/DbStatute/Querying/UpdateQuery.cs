using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Internals;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace DbStatute.Querying
{
    public abstract class UpdateQuery<TId, TModel> : FieldQualifier<TId, TModel>, IUpdateQuery<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly Dictionary<string, ReadOnlyLogbookPredicate<object>> _predicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
        private readonly TModel _updaterModel = new TModel();
        private readonly Dictionary<string, object> _valueMap = new Dictionary<string, object>();

        public IReadOnlyDictionary<string, ReadOnlyLogbookPredicate<object>> PredicateMap => _predicateMap;
        public TModel UpdaterModel => (TModel)_updaterModel.Clone();
        public IReadOnlyDictionary<string, object> ValueMap => _valueMap;

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
            MemberExpression propertyMember = property.ToMember();

            Type propertyType = propertyMember.GetMemberType();
            Type valueType = typeof(TValue);

            if (propertyType != valueType)
            {
                throw new InvalidTypeException("Property and value type not equals");
            }

            if (!propertyType.IsDbType())
            {
                throw new InvalidTypeException($"Property ({propertyType.FullName}) is not resolvable to DbType");
            }

            if (!valueType.IsDbType())
            {
                throw new InvalidTypeException($"Value ({valueType.FullName}) is not resolvable to DbType");
            }

            string propertyName = propertyMember.GetName();
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
            string propertyName = property?.ToMember().GetName();

            return _predicateMap.Remove(propertyName);
        }

        public override bool UnsetField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();

            return _valueMap.Remove(propertyName) || UnsetField(propertyName);
        }
    }
}