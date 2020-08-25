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
    public class FieldQualifier : IFieldQualifier
    {
        public FieldQualifier(IEnumerable<Field> fields)
        {
            Fields = fields;
        }

        public IEnumerable<Field> Fields { get; }
    }

    public class FieldQualifier<TId, TModel> : IFieldQualifier<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly HashSet<Field> _fields = new HashSet<Field>();

        public IEnumerable<Field> Fields => _fields;

        public void SetField<TProperty>(Expression<Func<TModel, TProperty>> property, Type type = null)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            // Need to check property value type for DbType
            Field field = new Field(propertyName, type);
            _fields.Add(field);
        }

        public bool UnsetField<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            int removeCount = _fields.RemoveWhere(x => x.Name == propertyName);

            return removeCount > 0;
        }
    }
}