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
    public class FieldQualifier : IFieldQualifier
    {
        private readonly HashSet<Field> _data = new HashSet<Field>();

        public FieldQualifier()
        {
            _data = new HashSet<Field>();
        }

        public FieldQualifier(IEnumerable<Field> fields)
        {
            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            _data = new HashSet<Field>(fields);
        }

        public int Count => _data.Count;

        public bool HasField => _data.Count > 0;

        public IEnumerable<Field> GetAllByName(string name)
        {
            return _data.Where(x => x.Name == name);
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            return _data.Where(x => x.Type == type);
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public IEnumerator<Field> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public bool IsSetted(Field field)
        {
            return _data.Contains(field);
        }

        public int IsSetted(string name)
        {
            return _data.Count(x => x.Name == name);
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            if (!_data.Add(field))
            {
                if (overrideEnabled)
                {
                    _data.Remove(field);
                    return _data.Add(field);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
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

    public class FieldQualifier<TModel> : FieldQualifier, IFieldQualifier<TModel>
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