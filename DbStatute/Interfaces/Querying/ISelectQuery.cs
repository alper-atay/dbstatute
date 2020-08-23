using Basiclog;
using RepoDb;
using System;

namespace DbStatute.Interfaces.Querying
{
    public interface ISelectQuery<TId, TModel> : IReadOnlyLogbookTestable
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
    {
        TModel Model { get; }

        QueryGroup QueryGroup { get; }
    }
}