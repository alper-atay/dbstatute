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
        private readonly Dictionary<Field, ReadOnlyLogbookPredicate<object>> _predicateMap = new Dictionary<Field, ReadOnlyLogbookPredicate<object>>();

        public IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> FieldPredicateMap => _predicateMap;

        public bool IsSetted(Field field)
        {
            return _predicateMap.ContainsKey(field);
        }

        public bool Set(Field field, ReadOnlyLogbookPredicate<object> value, bool overrideEnabled = false)
        {
            if (!_predicateMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _predicateMap.Remove(field);

                    return _predicateMap.TryAdd(field, value);
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
            if (!_predicateMap.TryAdd(field, default))
            {
                if (overrideEnabled)
                {
                    _predicateMap.Remove(field);
                    return _predicateMap.TryAdd(field, default);
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
            return _predicateMap.Remove(field);
        }
    }

    public class PredicateFieldQualifier<TModel> : IPredicateFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly Dictionary<Field, ReadOnlyLogbookPredicate<object>> _predicateMap;

        public IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> FieldPredicateMap => _predicateMap;

        public bool IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            bool isSetted = fields.Count() > 0;

            foreach (Field field in fields)
            {
                isSetted = isSetted && _predicateMap.ContainsKey(field);
            }

            return isSetted;
        }

        public bool IsSetted(Field field)
        {
            return _predicateMap.ContainsKey(field);
        }

        public bool Set(Expression<Func<TModel, object>> expression, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (!_predicateMap.TryAdd(field, predicate))
                {
                    if (overrideEnabled)
                    {
                        _predicateMap.Remove(field);

                        return _predicateMap.TryAdd(field, predicate);
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

        public bool Set(Field field, ReadOnlyLogbookPredicate<object> predicate, bool overrideEnabled = false)
        {
            if (!_predicateMap.TryAdd(field, predicate))
            {
                if (overrideEnabled)
                {
                    _predicateMap.Remove(field);

                    return _predicateMap.TryAdd(field, predicate);
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
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (!_predicateMap.TryAdd(field, default))
                {
                    if (overrideEnabled)
                    {
                        _predicateMap.Remove(field);

                        return _predicateMap.TryAdd(field, default);
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
            if (!_predicateMap.TryAdd(field, default))
            {
                if (overrideEnabled)
                {
                    _predicateMap.Remove(field);
                    return _predicateMap.TryAdd(field, default);
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
                if (_predicateMap.Remove(field))
                {
                    unsettedCount += 1;
                }
            }

            return unsettedCount > 0;
        }

        public bool Unset(Field field)
        {
            return _predicateMap.Remove(field);
        }
    }
}