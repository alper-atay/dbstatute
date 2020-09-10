using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Qualifiers
{
    public class ValueFieldQualifier : IValueFieldQualifier
    {
        private readonly Dictionary<Field, object> _data = new Dictionary<Field, object>();

        public int Count => _data.Count;

        public IEnumerable<Field> Keys => _data.Keys;

        public IEnumerable<object> Values => _data.Values;

        public object this[Field key] => _data[key];

        public bool ContainsKey(Field key)
        {
            return _data.ContainsKey(key);
        }

        public IEnumerable<Field> GetAllByName(string name)
        {
            Dictionary<Field, object>.KeyCollection fields = _data.Keys;

            return fields.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            Dictionary<Field, object>.KeyCollection fields = _data.Keys;

            return fields.Where(x => x.Type.Equals(type));
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public IEnumerator<KeyValuePair<Field, object>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public object GetValue(Field field)
        {
            return _data[field];
        }

        public bool IsSetted(Field field)
        {
            return _data.ContainsKey(field);
        }

        public int IsSetted(string name)
        {
            Dictionary<Field, object>.KeyCollection fields = _data.Keys;

            return fields.Count(x => x.Name.Equals(name));
        }

        public bool Set(Field field, object value, bool overrideEnabled = false)
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
            if (!_data.TryAdd(field, default))
            {
                if (overrideEnabled)
                {
                    _data.Remove(field);

                    return _data.TryAdd(field, default);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public int SetAll(IReadOnlyDictionary<Field, object> map, bool overrideEnabled = false)
        {
            int settedCount = 0;

            foreach (KeyValuePair<Field, object> pair in map)
            {
                if (Set(pair.Key, pair.Value, overrideEnabled))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public int SetAll(IEnumerable<Field> fields, bool overrideEnabled = false)
        {
            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, overrideEnabled))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public bool TryGetValue(Field key, out object value)
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

    public class ValueFieldQualifier<TModel> : ValueFieldQualifier, IValueFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public object GetValue(Expression<Func<TModel, object>> expression)
        {
            Field field = Field.Parse(expression).FirstOrDefault();

            return GetValue(field);
        }

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

        public int Set(Expression<Func<TModel, object>> expression, object value, bool overrideEnabled = false)
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
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, overrideEnabled))
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