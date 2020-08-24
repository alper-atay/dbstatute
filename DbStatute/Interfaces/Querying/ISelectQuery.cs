using Basiclog;
using RepoDb;
using System;

namespace DbStatute.Interfaces.Querying
{
    public interface ISelectQuery<TId, TModel> : IReadOnlyLogbookTestable
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        TModel SelecterModel { get; }

        QueryGroup QueryGroup { get; }
    }
}