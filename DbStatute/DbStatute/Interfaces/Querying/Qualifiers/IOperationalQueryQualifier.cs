﻿using Basiclog;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IOperationalQueryQualifier : IQueryQualifier, IFieldOperationMap
    {
        Conjunction Conjunction { get; set; }

        protected static IReadOnlyLogbook GetQueryGroup(IOperationalQueryQualifier @this, out QueryGroup queryGroup)
        {
            queryGroup = null;

            ILogbook logs = Logger.NewLogbook();

            IFieldBuilder fieldQualifier = @this.FieldQualifier;
            IEnumerable<Field> fields = fieldQualifier.Fields;

            ICollection<QueryField> queryFields = new Collection<QueryField>();

            foreach (Field field in fields)
            {
                string name = field.Name;
                bool valueFound = @this.FieldValueMap.TryGetValue(name, out object value);
                bool predicateFound = @this.FieldPredicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate);
                bool operationFound = @this.FieldOperationMap.TryGetValue(name, out Operation operation);

                if (valueFound)
                {
                    QueryField queryField = new QueryField(field, value);
                    queryFields.Add(queryField);
                }

                if (valueFound && predicateFound)
                {
                    logs.AddRange(predicate.Invoke(value));
                }

                if (valueFound && operationFound)
                {
                    QueryField queryField = new QueryField(field, operation, value);
                    queryFields.Add(queryField);
                }
            }

            int queryFieldCount = queryFields.Count;

            if (queryFieldCount > 0)
            {
                queryGroup = new QueryGroup(queryFields, @this.Conjunction);
            }

            return logs;
        }
    }

    public interface IOperationalQueryQualifier<TModel> : IOperationalQueryQualifier, IQueryQualifier<TModel>
        where TModel : class, IModel, new()
    {
        bool SetOperation(Expression<Func<TModel, object>> expression, Operation operation);

        bool SetOperation(string name, Operation operation);
    }
}