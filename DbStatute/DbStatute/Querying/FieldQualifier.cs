using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public class FieldQualifier<TModel> : IFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly HashSet<Field> _fields = new HashSet<Field>();

        public IEnumerable<Field> Fields => _fields;

        public bool IsFieldSetted(Expression<Func<TModel, object>> property)
        {
            Field field = Field.Parse(property).Single();

            return _fields.Contains(field);
        }

        public bool IsFieldSetted(string propertyName, Type propertyType = null)
        {
            Field field = new Field(propertyName, propertyType);

            return _fields.Contains(field);
        }

        public bool SetField(Expression<Func<TModel, object>> property, bool overrideEnabled = false)
        {
            if (IsFieldSetted(property))
            {
                if (overrideEnabled)
                {
                    UnsetField(property);
                }
                else
                {
                    return false;
                }
            }

            Field field = Field.Parse(property).Single();

            return _fields.Add(field);
        }

        public bool SetField(string propertyName, Type propertyType = null, bool overrideEnabled = false)
        {
            if (IsFieldSetted(propertyName))
            {
                if (overrideEnabled)
                {
                    UnsetField(propertyName);
                }
                else
                {
                    return false;
                }
            }

            Field field = new Field(propertyName, propertyType);

            return _fields.Add(field);
        }

        public bool UnsetField(Expression<Func<TModel, object>> property)
        {
            Field field = Field.Parse(property).Single();

            return _fields.Remove(field);
        }

        public bool UnsetField(string propertyName, Type propertyType = null)
        {
            Field field = new Field(propertyName, propertyType);

            return _fields.Remove(field);
        }

        public bool UnsetField(string propertyName)
        {
            int removeCount = _fields.RemoveWhere(x => x.Name == propertyName);

            return removeCount > 0;
        }
    }
}