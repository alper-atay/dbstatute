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
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly IDictionary<string, ReadOnlyLogbookPredicate<object>> _propertyPredicateMap = new Dictionary<string, ReadOnlyLogbookPredicate<object>>();
        private readonly List<PropertyNameValuePredicateTriple> _triples = new List<PropertyNameValuePredicateTriple>();
        public TModel Model { get; } = new TModel();
        public IEnumerable<Field> UpdateFields => GetUpdateFields();

        public bool IsEnableUpdateField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            int propertyNameHashCode = propertyName.GetHashCode();

            return _triples.Exists(x => x.Name.GetHashCode() == propertyNameHashCode);
        }

        public void SetUpdateField<TValue>(Expression<Func<TModel, TValue>> property, TValue value, ReadOnlyLogbookPredicate<object> predicate)
        {
            Type valueType = typeof(TValue);
            if (!valueType.IsDbType())
            {
                throw new InvalidTypeException($"Value ({valueType.FullName}) is not resolvable to DbType");
            }

            string propertyName = property.ToMember()?.GetName();
            int propertyNameHashCode = propertyName.GetHashCode();

            if (_triples.Exists(x => x.Name.GetHashCode() == propertyNameHashCode))
            {
                _triples.RemoveAll(x => x.Name.GetHashCode() == propertyNameHashCode);
            }

            _triples.Add(new PropertyNameValuePredicateTriple(propertyName, value, predicate));

            Type modelType = typeof(TModel);
            PropertyInfo propertyInfo = modelType.GetProperty(propertyName);
            propertyInfo.SetValue(Model, value);
        }

        public IReadOnlyLogbook Test()
        {
            ILogbook logs = Logger.New();

            foreach (PropertyNameValuePredicateTriple triple in _triples)
            {
                logs.AddRange(triple.Predicate.Invoke(triple.Value));
            }

            return logs;
        }

        public void UnsetUpdateField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            int propertyNameHasCode = propertyName.GetHashCode();

            _triples.RemoveAll(x => x.Name.GetHashCode() == propertyNameHasCode);
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

        private IEnumerable<Field> GetUpdateFields()
        {
            if (_triples.Count == 0)
            {
                return null;
            }

            ICollection<Field> fields = new Collection<Field>();

            foreach (PropertyNameValuePredicateTriple triple in _triples)
            {
                fields.Add(new Field(triple.Name));
            }

            return fields;
        }
    }
}