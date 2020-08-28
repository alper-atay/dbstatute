using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers.Fields
{
    public class ValueFieldQualifier : IValueFieldQualifier
    {
        private readonly Dictionary<Field, object> _valueMap = new Dictionary<Field, object>();

        public IFieldQualifier FieldQualifier { get; }
        public IReadOnlyDictionary<Field, object> FieldValueMap => _valueMap;

        public bool IsSetted(Field field)
        {
            return _valueMap.ContainsKey(field);
        }

        public bool Set(Field field, object value, bool overrideEnabled = false)
        {
            if (!_valueMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _valueMap.Remove(field);

                    return _valueMap.TryAdd(field, value);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            if (!_valueMap.TryAdd(field, default))
            {
                if (overrideEnabled)
                {
                    _valueMap.Remove(field);

                    return _valueMap.TryAdd(field, default);
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
            return _valueMap.Remove(field);
        }
    }

    public class ValueFieldQualifier<TModel> : IValueFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly Dictionary<Field, object> _valueMap = new Dictionary<Field, object>();

        public IReadOnlyDictionary<Field, object> FieldValueMap => _valueMap;

        public bool IsSetted(Expression<Func<TModel, object>> expression)
        {
            throw new NotImplementedException();
        }

        public bool IsSetted(Field field)
        {
            return _valueMap.ContainsKey(field);
        }

        public bool Set(Expression<Func<TModel, object>> expression, object value, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (!_valueMap.TryAdd(field, value))
                {
                    if (overrideEnabled)
                    {
                        _valueMap.Remove(field);

                        return _valueMap.TryAdd(field, value);
                    }
                    {
                        continue;
                    }
                }

                settedCount += 1;
            }

            return settedCount > 0;
        }

        public bool Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (!_valueMap.TryAdd(field, default))
                {
                    if (overrideEnabled)
                    {
                        _valueMap.Remove(field);

                        return _valueMap.TryAdd(field, default);
                    }
                    {
                        continue;
                    }
                }

                settedCount += 1;
            }

            return settedCount > 0;
        }

        public bool Set(Field field, object value, bool overrideEnabled = false)
        {
            if (!_valueMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _valueMap.Remove(field);

                    return _valueMap.TryAdd(field, value);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            if (!_valueMap.TryAdd(field, default))
            {
                if (overrideEnabled)
                {
                    _valueMap.Remove(field);

                    return _valueMap.TryAdd(field, default);
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
                if (_valueMap.Remove(field))
                {
                    unsettedCount += 1;
                }
            }

            return unsettedCount > 0;
        }

        public bool Unset(Field field)
        {
            return _valueMap.Remove(field);
        }
    }
}