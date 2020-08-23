using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Internals;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace DbStatute.Querying
{

    public abstract class UpdateQuery<TId, TModel> : IUpdateQuery
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly List<PropertyNameValuePredicateTriple<dynamic>> _fieldMap = new List<PropertyNameValuePredicateTriple<dynamic>>();

        public IEnumerable<Field> UpdateFields => GetUpdateFields();

        public TModel Model { get; } = new TModel();

        public bool IsEnableUpdateField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            return true;
        }

        // TODO
        // Setting a will update field
        public void SetUpdateField<TValue>(Expression<Func<TModel, TValue>> property, TValue value, ReadOnlyLogbookPredicate<TValue> predicate)
        {
            // First mission is lambda to
            // property lambda to property name -> x.Nick to Nick and check in List
            //
            //

            string propertyName = string.Empty;
            int propertyNameHashCode = propertyName.GetHashCode();

            if (_fieldMap.Exists(x => x.GetHashCode() == propertyNameHashCode))
            {
                //already exists and will be delete

                _fieldMap.RemoveAll(x => x.GetHashCode() == propertyNameHashCode);
            }

            // Problem predicate can not castting? How can i fix that.
            // Predicate uses purpose rules
            // For example Nick cannot contain letters other than the English alphabet.
            // Or Special character like space, star, / \ etc.
            _fieldMap.Add(new PropertyNameValuePredicateTriple<dynamic>(propertyName, value, predicate));

            // We must be have TModel property setting with lambda
            // Model.Nick = value :(
            // How can i make?



            // And then calling GetUpdateFields
        }

        public IReadOnlyLogbook Test()
        {
            ILogbook logs = Logger.New();

            // Automatically predicate the value there
            foreach (PropertyNameValuePredicateTriple<dynamic> triple in _fieldMap)
            {
                logs.AddRange(triple.Predicate.Invoke(triple.Value));
            }

            return logs;
        }

        public void UnsetUpdateField<TValue>(Expression<Func<TModel, TValue>> property)
        {
        }

        private IEnumerable<Field> GetUpdateFields()
        {

            // If _fieldMap count is zero
            // We dont have any fields
            // Return to as null
            // It is not problem we will be control in Update
            if (_fieldMap.Count == 0)
            {
                return null;
            }

            ICollection<Field> fields = new Collection<Field>();

            foreach (PropertyNameValuePredicateTriple<dynamic> triple in _fieldMap)
            {
                fields.Add(new Field(triple.Name));
            }

            return fields;
        }
    }
}