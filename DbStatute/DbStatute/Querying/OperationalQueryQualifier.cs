using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace DbStatute.Querying
{
    public class OperationalQueryQualifier<TId, TModel> : QueryQualifier<TId, TModel>, IOperationalQueryQualifier<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly Dictionary<string, Operation> _operationMap = new Dictionary<string, Operation>();

        public Conjunction Conjunction { get; set; }
        public IReadOnlyDictionary<string, Operation> OperationMap => _operationMap;

        public override IReadOnlyLogbook BuildQueryGroup(out QueryGroup queryGroup)
        {
            queryGroup = null;

            ILogbook logs = Logger.NewLogbook();

            IFieldQualifier fieldQualifier = GetFieldQualifier();
            IEnumerable<Field> fields = fieldQualifier.Fields;

            ICollection<QueryField> queryFields = new Collection<QueryField>();

            foreach (Field field in fields)
            {
                string name = field.Name;
                bool valueFound = ValueMap.TryGetValue(name, out object value);
                bool predicateFound = PredicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate);
                bool operationFound = OperationMap.TryGetValue(name, out Operation operation);

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
                queryGroup = new QueryGroup(queryFields, Conjunction);
            }

            return logs;
        }

        public bool SetOperation(Expression<Func<TModel, object>> property, Operation operation)
        {
            string propertyName = property.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _operationMap.TryAdd(propertyName, operation);
        }

        public bool SetOperation(string propertyName, Operation operation)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or white space", nameof(propertyName));
            }

            return _operationMap.TryAdd(propertyName, operation);
        }
    }
}