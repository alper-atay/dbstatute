using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers
{
    public class FieldQualifier : IFieldQualifier
    {
        private readonly HashSet<Field> _fields;

        public FieldQualifier()
        {
            _fields = new HashSet<Field>();
        }

        public FieldQualifier(IEnumerable<Field> fields)
        {
            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            _fields = new HashSet<Field>(fields);
        }

        public IEnumerable<Field> Fields => _fields;
        public bool HasField => Fields.Count() > 0;

        public bool IsSetted(Field field)
        {
            return _fields.Contains(field);
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            if (!_fields.Add(field))
            {
                if (overrideEnabled)
                {
                    _fields.Remove(field);
                    return _fields.Add(field);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Unset(Field field)
        {
            return _fields.Remove(field);
        }
    }

    public class FieldQualifier<TModel> : IFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly HashSet<Field> _fields = new HashSet<Field>();

        public FieldQualifier()
        {
            _fields = new HashSet<Field>();
        }

        public FieldQualifier(IEnumerable<Field> fields)
        {
            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            _fields = new HashSet<Field>(fields);
        }

        public IEnumerable<Field> Fields => _fields;
        public bool HasField => _fields.Count > 0;

        public bool IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (_fields.Contains(field))
                {
                    settedCount += 1;
                }
            }

            return settedCount > 0;
        }

        public bool IsSetted(Field field)
        {
            return _fields.Contains(field);
        }

        public bool Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (!_fields.Add(field))
                {
                    if (overrideEnabled)
                    {
                        _fields.Remove(field);
                        _fields.Add(field);
                    }
                    else
                    {
                        continue;
                    }
                }

                settedCount += 1;
            }

            return settedCount > 0;
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            if (!_fields.Add(field))
            {
                if (overrideEnabled)
                {
                    _fields.Remove(field);
                    _fields.Add(field);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Unset(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int unsettedCount = 0;

            foreach (Field field in fields)
            {
                if (_fields.Remove(field))
                {
                    unsettedCount += 1;
                }
            }

            return unsettedCount > 0;
        }

        public bool Unset(Field field)
        {
            return _fields.Remove(field);
        }
    }
}