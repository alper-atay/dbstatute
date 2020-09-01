using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Qualifiers
{
    public class ValueFieldQualifier : IValueFieldQualifier
    {
        public IFieldQualifier FieldQualifier { get; }

        public IReadOnlyDictionary<Field, object> FieldValuePairs => FieldValueDictionary;

        protected Dictionary<Field, object> FieldValueDictionary { get; } = new Dictionary<Field, object>();

        public IEnumerable<Field> GetAllByName(string name)
        {
            Dictionary<Field, object>.KeyCollection fields = FieldValueDictionary.Keys;

            return fields.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            Dictionary<Field, object>.KeyCollection fields = FieldValueDictionary.Keys;

            return fields.Where(x => x.Type.Equals(type));
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public bool IsSetted(Field field)
        {
            return FieldValueDictionary.ContainsKey(field);
        }

        public int IsSetted(string name)
        {
            Dictionary<Field, object>.KeyCollection fields = FieldValueDictionary.Keys;

            return fields.Count(x => x.Name.Equals(name));
        }

        public bool Set(Field field, object value, bool overrideEnabled = false)
        {
            if (!FieldValueDictionary.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    FieldValueDictionary.Remove(field);

                    return FieldValueDictionary.TryAdd(field, value);
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
            if (!FieldValueDictionary.TryAdd(field, default))
            {
                if (overrideEnabled)
                {
                    FieldValueDictionary.Remove(field);

                    return FieldValueDictionary.TryAdd(field, default);
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
            return FieldValueDictionary.Remove(field);
        }
    }

    public class ValueFieldQualifier<TModel> : ValueFieldQualifier, IValueFieldQualifier<TModel>
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
                if (FieldValueDictionary.Remove(field))
                {
                    unsettedCount += 1;
                }
            }

            return unsettedCount;
        }
    }
}