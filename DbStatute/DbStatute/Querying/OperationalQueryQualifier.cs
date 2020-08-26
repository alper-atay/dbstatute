using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Querying
{
    public class OperationalQueryQualifier : QueryQualifier, IOperationalQueryQualifier
    {
        public OperationalQueryQualifier(IReadOnlyDictionary<string, Operation> operationMap)
        {
            OperationMap = operationMap;
        }

        public Conjunction Conjunction { get; set; }

        public IReadOnlyDictionary<string, Operation> OperationMap { get; }

        public override IReadOnlyLogbook GetQueryGroup(out QueryGroup queryGroup)
        {
            return IOperationalQueryQualifier.GetQueryGroup(this, out queryGroup);
        }
    }

    public class OperationalQueryQualifier<TModel> : QueryQualifier<TModel>, IOperationalQueryQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly Dictionary<string, Operation> _operationMap = new Dictionary<string, Operation>();

        public Conjunction Conjunction { get; set; }
        public IReadOnlyDictionary<string, Operation> OperationMap => _operationMap;

        public override IReadOnlyLogbook GetQueryGroup(out QueryGroup queryGroup)
        {
            return IOperationalQueryQualifier.GetQueryGroup(this, out queryGroup);
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