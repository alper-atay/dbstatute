using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Qualifiers
{
    public class QueryFieldQualifier : IQueryFieldQualifier
    {
        private readonly HashSet<QueryField> _queryFields = new HashSet<QueryField>();

        public bool HasQueryField => _queryFields.Count > 0;

        public IEnumerable<QueryField> ReadOnlyQueryFields => HasQueryField ? _queryFields : null;

        public int IsSetted(Field field)
        {
            return _queryFields.Count(x => x.Field.Equals(field));
        }

        public bool IsSetted(QueryField queryField)
        {
            return _queryFields.Contains(queryField);
        }

        public bool Set(Field field, object value, bool overrideEnabled = false)
        {
            int settedFieldCount = IsSetted(field);

            if (settedFieldCount > 0)
            {
                if (overrideEnabled)
                {
                    _queryFields.RemoveWhere(x => x.Field == field);
                }
                else
                {
                    return false;
                }
            }

            QueryField queryField = new QueryField(field, value);
            return _queryFields.Add(queryField);
        }

        public bool Set(QueryField queryField, bool overrideEnabled = false)
        {
            if (IsSetted(queryField))
            {
                if (overrideEnabled)
                {
                    _queryFields.Remove(queryField);
                }
                else
                {
                    return false;
                }
            }

            return _queryFields.Add(queryField);
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            int settedFieldCount = IsSetted(field);

            if (settedFieldCount > 0)
            {
                if (overrideEnabled)
                {
                    int removedCount = _queryFields.RemoveWhere(x => x.Field.Equals(field));
                }
                else
                {
                    return false;
                }
            }

            QueryField queryField = new QueryField(field, default);
            return _queryFields.Add(queryField);
        }

        public bool Set(Field field, Operation operation, bool overrideEnabled = false)
        {
            int settedFieldCount = IsSetted(field);

            if (settedFieldCount > 0)
            {
                if (overrideEnabled)
                {
                    _queryFields.RemoveWhere(x => x.Field == field);
                }
                else
                {
                    return false;
                }
            }

            QueryField queryField = new QueryField(field, operation, default);
            return _queryFields.Add(queryField);
        }

        public bool Set(Field field, Operation operation, object value, bool overrideEnabled = false)
        {
            int settedFieldCount = IsSetted(field);

            if (settedFieldCount > 0)
            {
                if (overrideEnabled)
                {
                    _queryFields.RemoveWhere(x => x.Field == field);
                }
                else
                {
                    return false;
                }
            }

            QueryField queryField = new QueryField(field, operation, value);
            return _queryFields.Add(queryField);
        }

        public int SettedCount(Field field)
        {
            return _queryFields.Count(x => x.Field.Equals(field));
        }

        public int Unset(Field field)
        {
            return _queryFields.RemoveWhere(x => x.Field.Equals(field));
        }

        public bool Unset(QueryField queryField)
        {
            return _queryFields.Remove(queryField);
        }
    }

    public class QueryFieldQualifier<TModel> : QueryFieldQualifier, IQueryFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public int IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                settedCount += IsSetted(field);
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

        public int Set(Expression<Func<TModel, object>> expression, Operation operation, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, operation, overrideEnabled))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public int Set(Expression<Func<TModel, object>> expression, Operation operation, object value, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, operation, value, overrideEnabled))
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
                unsettedCount += Unset(field);
            }

            return unsettedCount;
        }
    }
}