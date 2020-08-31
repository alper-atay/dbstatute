using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers.Fields
{
    public class ValueFieldQualifier : IValueFieldQualifier
    {
        public IFieldQualifier FieldQualifier { get; }
        public IReadOnlyDictionary<Field, object> ReadOnlyFieldValueMap => FieldValueMap;
        protected Dictionary<Field, object> FieldValueMap { get; } = new Dictionary<Field, object>();

        public IEnumerable<Field> GetAllByName(string name)
        {
            Dictionary<Field, object>.KeyCollection fields = FieldValueMap.Keys;

            return fields.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            Dictionary<Field, object>.KeyCollection fields = FieldValueMap.Keys;

            return fields.Where(x => x.Type.Equals(type));
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public bool IsSetted(Field field)
        {
            return FieldValueMap.ContainsKey(field);
        }

        public int IsSetted(string name)
        {
            Dictionary<Field, object>.KeyCollection fields = FieldValueMap.Keys;

            return fields.Count(x => x.Name.Equals(name));
        }

        public bool Set(Field field, object value, bool overrideEnabled = false)
        {
            if (!FieldValueMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    FieldValueMap.Remove(field);

                    return FieldValueMap.TryAdd(field, value);
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
            if (!FieldValueMap.TryAdd(field, default))
            {
                if (overrideEnabled)
                {
                    FieldValueMap.Remove(field);

                    return FieldValueMap.TryAdd(field, default);
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
            return FieldValueMap.Remove(field);
        }
    }

    public class ValueFieldQualifier<TModel> : ValueFieldQualifier, IValueFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public int IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (var field in fields)
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
                if (FieldValueMap.Remove(field))
                {
                    unsettedCount += 1;
                }
            }

            return unsettedCount;
        }
    }
}