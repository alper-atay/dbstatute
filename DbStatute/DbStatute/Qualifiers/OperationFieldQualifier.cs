using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Qualifiers
{
    public class OperationFieldQualifier : IOperationFieldQualifier
    {
        private readonly Dictionary<Field, Operation> _data = new Dictionary<Field, Operation>();

        public int Count => _data.Count;

        public IEnumerable<Field> Keys => _data.Keys;

        public IEnumerable<Operation> Values => _data.Values;

        public Operation this[Field key] => _data[key];

        public bool ContainsKey(Field key)
        {
            return _data.ContainsKey(key);
        }

        public IEnumerable<Field> GetAllByName(string name)
        {
            Dictionary<Field, Operation>.KeyCollection fields = _data.Keys;

            return fields.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            Dictionary<Field, Operation>.KeyCollection fields = _data.Keys;

            return fields.Where(x => x.Type.Equals(type));
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public IEnumerator<KeyValuePair<Field, Operation>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public bool IsSetted(Field field)
        {
            Dictionary<Field, Operation>.KeyCollection fields = _data.Keys;

            return fields.Contains(field);
        }

        public int IsSetted(string name)
        {
            Dictionary<Field, Operation>.KeyCollection fields = _data.Keys;

            return fields.Count(x => x.Name.Equals(name));
        }

        public bool Set(Field field, Operation value, bool overrideEnabled = false)
        {
            if (!_data.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _data.Remove(field);

                    return _data.TryAdd(field, value);
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

            if (!_data.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _data.Remove(field);

                    return _data.TryAdd(field, value);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool TryGetValue(Field key, out Operation value)
        {
            throw new NotImplementedException();
        }

        public bool Unset(Field field)
        {
            return _data.Remove(field);
        }

        public int UnsetAll()
        {
            int unsettedCount = _data.Count;

            _data.Clear();

            return unsettedCount;
        }
    }

    public class OperationFieldQualifier<TModel> : OperationFieldQualifier, IOperationFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public int IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (IsSetted(field))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public int Set(Expression<Func<TModel, object>> expression, Operation value, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, value, overrideEnabled))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public int Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false)
        {
            const Operation value = Operation.Equal;

            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, value, overrideEnabled))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public int Unset(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int unsettedCount = 0;

            foreach (Field field in fields)
            {
                if (Unset(field))
                {
                    unsettedCount += 1;
                }
            }

            return unsettedCount;
        }
    }
}