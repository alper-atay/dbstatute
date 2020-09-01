using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IQueryFields
    {
        bool HasQueryField { get; }

        IEnumerable<QueryField> QueryFields { get; }
    }
}