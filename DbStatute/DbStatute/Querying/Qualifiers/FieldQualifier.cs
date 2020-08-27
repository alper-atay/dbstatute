using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers
{
    public class FieldQualifier : IFieldQualifier
    {
        public FieldQualifier(IEnumerable<Field> fields)
        {
            Fields = fields;
        }

        public IEnumerable<Field> Fields { get; }
        public bool HasField => Fields.Count() > 0;
    }

    public class FieldQualifier<TModel> : IFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly HashSet<Field> _fields = new HashSet<Field>();

        public IEnumerable<Field> Fields => _fields;
        public bool HasField => Fields.Count() > 0;

        public bool IsFieldSetted(Expression<Func<TModel, object>> expression)
        {
            Field field = Field.Parse(expression).Single();

            return _fields.Contains(field);
        }

        public bool IsFieldSetted(string name, Type type = null)
        {
            Field field = new Field(name, type);

            return _fields.Contains(field);
        }

        public bool SetField(Expression<Func<TModel, object>> expression, bool overrideEnabled = false)
        {
            if (IsFieldSetted(expression))
            {
                if (overrideEnabled)
                {
                    UnsetField(expression);
                }
                else
                {
                    return false;
                }
            }

            Field field = Field.Parse(expression).Single();

            return _fields.Add(field);
        }

        public bool SetField(string name, Type type = null, bool overrideEnabled = false)
        {
            if (IsFieldSetted(name))
            {
                if (overrideEnabled)
                {
                    UnsetField(name);
                }
                else
                {
                    return false;
                }
            }

            Field field = new Field(name, type);

            return _fields.Add(field);
        }

        public bool UnsetField(Expression<Func<TModel, object>> expression)
        {
            Field field = Field.Parse(expression).Single();

            return _fields.Remove(field);
        }

        public bool UnsetField(string name, Type type = null)
        {
            Field field = new Field(name, type);

            return _fields.Remove(field);
        }

        public bool UnsetField(string name)
        {
            int removeCount = _fields.RemoveWhere(x => x.Name == name);

            return removeCount > 0;
        }
    }
}