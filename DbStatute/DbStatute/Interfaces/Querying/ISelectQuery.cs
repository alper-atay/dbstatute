using Basiclog;
using RepoDb;
using System;

namespace DbStatute.Interfaces.Querying
{
    public interface ISelectQuery : IReadOnlyLogbookTestable
    {
        QueryGroup QueryGroup { get; }
    }

    public interface ISelectQuery<TId, TModel> : ISelectQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        TModel SelecterModel { get; }
    }
}