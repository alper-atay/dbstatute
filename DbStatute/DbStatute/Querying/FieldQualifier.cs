using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
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

        public bool IsFieldSetted<TValue>(Expression<Func<TModel, TValue>> property)
        {
            Field field = property.ToMember().GetField();

            return _fields.Contains(field);
        }

        public virtual void SetField<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            Field field = property.ToMember().GetField();

            _fields.Add(field);
        }

        public virtual bool UnsetField<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            Field field = property.ToMember().GetField();

            return _fields.Remove(field);
        }

        protected bool UnsetField(string propertyName)
        {
            int removeCount = _fields.RemoveWhere(x => x.Name == propertyName);

            return removeCount > 0;
        }
    }
}