using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying
{
    public interface IOperationalQueryQualifier : IQueryQualifier
    {
        Conjunction Conjunction { get; set; }
        IReadOnlyDictionary<string, Operation> OperationMap { get; }
    }

    public interface IOperationalQueryQualifier<TId, TModel> : IOperationalQueryQualifier, IQueryQualifier<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        bool SetOperation(Expression<Func<TModel, object>> property, Operation operation);

        bool SetOperation(string propertyName, Operation operation);
    }
}