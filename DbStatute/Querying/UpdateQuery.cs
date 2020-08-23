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
    /// <summary>
    /// Need to test
    ///
    ///
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TModel"></typeparam>

    public abstract class UpdateQuery<TId, TModel> : IUpdateQuery
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly List<PropertyNameValuePredicateTriple> _fieldMap = new List<PropertyNameValuePredicateTriple>();

        public TModel Model { get; } = new TModel();
        public IEnumerable<Field> UpdateFields => GetUpdateFields();

        public bool IsEnableUpdateField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            int propertyNameHasCode = propertyName.GetHashCode();

            return _fieldMap.Exists(x => x.Name.GetHashCode() == propertyNameHasCode);
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

            if (_fieldMap.Exists(x => x.Name.GetHashCode() == propertyNameHashCode))
            {
                _fieldMap.RemoveAll(x => x.Name.GetHashCode() == propertyNameHashCode);
            }

            _fieldMap.Add(new PropertyNameValuePredicateTriple(propertyName, value, predicate));

            Type modelType = typeof(TModel);
            PropertyInfo propertyInfo = modelType.GetProperty(propertyName);
            propertyInfo.SetValue(Model, value);
        }

        public IReadOnlyLogbook Test()
        {
            ILogbook logs = Logger.New();

            foreach (PropertyNameValuePredicateTriple triple in _fieldMap)
            {
                logs.AddRange(triple.Predicate.Invoke(triple.Value));
            }

            return logs;
        }

        public void UnsetUpdateField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            int propertyNameHasCode = propertyName.GetHashCode();

            _fieldMap.RemoveAll(x => x.Name.GetHashCode() == propertyNameHasCode);
        }

        private IEnumerable<Field> GetUpdateFields()
        {
            if (_fieldMap.Count == 0)
            {
                return null;
            }

            ICollection<Field> fields = new Collection<Field>();

            foreach (PropertyNameValuePredicateTriple triple in _fieldMap)
            {
                fields.Add(new Field(triple.Name));
            }

            return fields;
        }
    }
}