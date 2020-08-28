using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers
{
    public class OperationalQueryQualifier : QueryQualifier, IOperationalQueryQualifier
    {
        public OperationalQueryQualifier(IReadOnlyDictionary<string, Operation> operationMap)
        {
            OperationMap = operationMap;
        }

        public Conjunction Conjunction { get; set; }

        public IReadOnlyDictionary<string, Operation> OperationMap { get; }
    }

    public class OperationalQueryQualifier<TModel> : QueryQualifier<TModel>, IOperationalQueryQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly Dictionary<string, Operation> _operationMap = new Dictionary<string, Operation>();

        public Conjunction Conjunction { get; set; }
        public IReadOnlyDictionary<string, Operation> OperationMap => _operationMap;

        public bool SetOperation(Expression<Func<TModel, object>> expression, Operation operation)
        {
            string name = expression.ToMember()?.GetName();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new PropertyNotFoundException($"{typeof(TModel).FullName} has not property");
            }

            return _operationMap.TryAdd(name, operation);
        }

        public bool SetOperation(string name, Operation operation)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or white space", nameof(name));
            }

            return _operationMap.TryAdd(name, operation);
        }
    }
}