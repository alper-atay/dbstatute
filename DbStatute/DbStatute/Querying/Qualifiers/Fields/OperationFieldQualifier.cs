using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers.Fields
{
    public class OperationFieldQualifier : IOperationFieldQualifier
    {
        private readonly Dictionary<Field, Operation> _fieldOperationMap = new Dictionary<Field, Operation>();

        public IReadOnlyDictionary<Field, Operation> FieldOperationMap => _fieldOperationMap;

        public bool IsSetted(Field field)
        {
            return _fieldOperationMap.ContainsKey(field);
        }

        public bool Set(Field field, Operation value, bool overrideEnabled = false)
        {
            if (!_fieldOperationMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _fieldOperationMap.Remove(field);

                    return _fieldOperationMap.TryAdd(field, value);
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
            const Operation value = Operation.Equal;

            if (!_fieldOperationMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _fieldOperationMap.Remove(field);

                    return _fieldOperationMap.TryAdd(field, value);
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
            return _fieldOperationMap.Remove(field);
        }
    }

    public class OperationFieldQualifier<TModel> : IOperationFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly Dictionary<Field, Operation> _fieldOperationMap = new Dictionary<Field, Operation>();

        public IReadOnlyDictionary<Field, Operation> FieldOperationMap => _fieldOperationMap;

        public bool IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            bool isSetted = fields.Count() > 0;

            foreach (Field field in fields)
            {
                isSetted = isSetted && _fieldOperationMap.ContainsKey(field);
            }

            return isSetted;
        }

        public bool IsSetted(Field field)
        {
            return _fieldOperationMap.ContainsKey(field);
        }

        public bool Set(Expression<Func<TModel, object>> expression, Operation value, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int setCount = 0;

            foreach (Field field in fields)
            {
                if (!_fieldOperationMap.TryAdd(field, value))
                {
                    if (overrideEnabled)
                    {
                        _fieldOperationMap.Remove(field);

                        if (!_fieldOperationMap.TryAdd(field, default))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                setCount += 1;
            }

            return setCount > 0;
        }

        public bool Set(Field field, Operation value, bool overrideEnabled = false)
        {
            if (!_fieldOperationMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _fieldOperationMap.Remove(field);

                    return _fieldOperationMap.TryAdd(field, value);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false)
        {
            const Operation value = Operation.Equal;

            IEnumerable<Field> fields = Field.Parse(expression);

            int setCount = 0;

            foreach (Field field in fields)
            {
                if (!_fieldOperationMap.TryAdd(field, value))
                {
                    if (overrideEnabled)
                    {
                        _fieldOperationMap.Remove(field);

                        if (!_fieldOperationMap.TryAdd(field, value))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                setCount += 1;
            }

            return setCount > 0;
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            const Operation value = Operation.Equal;

            if (!_fieldOperationMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _fieldOperationMap.Remove(field);

                    return _fieldOperationMap.TryAdd(field, value);
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

            int unsetCount = 0;

            foreach (Field field in fields)
            {
                if (_fieldOperationMap.Remove(field))
                {
                    unsetCount += 1;
                }
            }

            return unsetCount > 0;
        }

        public bool Unset(Field field)
        {
            return _fieldOperationMap.Remove(field);
        }
    }
}