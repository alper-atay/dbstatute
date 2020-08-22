using Basiclog;
using RepoDb;
using System;

namespace DbStatute.Interfaces.Querying
{
    public interface ISelectQuery<TId> : IModel<TId>, IReadOnlyLogbookTestable
        where TId : struct, IConvertible
    {
        QueryGroup QueryGroup { get; }
    }
}