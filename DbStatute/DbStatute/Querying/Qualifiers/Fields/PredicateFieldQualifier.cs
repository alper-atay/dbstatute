using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers.Fields
{
    public class PredicateFieldQualifier : IPredicateFieldQualifier
    {
        public IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> ReadOnlyFieldPredicateMap => FieldPredicateMap;
        protected Dictionary<Field, ReadOnlyLogbookPredicate<object>> FieldPredicateMap { get; } = new Dictionary<Field, ReadOnlyLogbookPredicate<object>>();

        public IEnumerable<Field> GetAllByName(string name)
        {
            Dictionary<Field, ReadOnlyLogbookPredicate<object>>.KeyCollection fields = FieldPredicateMap.Keys;

            return fields.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            Dictionary<Field, ReadOnlyLogbookPredicate<object>>.KeyCollection fields = FieldPredicateMap.Keys;

            return fields.Where(x => x.Type.Equals(type));
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public bool IsSetted(Field field)
        {
            return FieldPredicateMap.ContainsKey(field);
        }

        public int IsSetted(string name)
        {
            Dictionary<Field, ReadOnlyLogbookPredicate<object>>.KeyCollection fields = FieldPredicateMap.Keys;

            return fields.Count(x => x.Name.Equals(name));
        }

        public bool Set(Field field, ReadOnlyLogbookPredicate<object> value, bool overrideEnabled = false)
        {
            if (!FieldPredicateMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    FieldPredicateMap.Remove(field);

                    return FieldPredicateMap.TryAdd(field, value);
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
            if (!FieldPredicateMap.TryAdd(field, default))
            {
                if (overrideEnabled)
                {
                    FieldPredicateMap.Remove(field);
                    return FieldPredicateMap.TryAdd(field, default);
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
            return FieldPredicateMap.Remove(field);
        }
    }

    public class PredicateFieldQualifier<TModel> : PredicateFieldQualifier, IPredicateFieldQualifier<TModel>
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

        public int Set(Expression<Func<TModel, object>> expression, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, predicate, overrideEnabled))
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