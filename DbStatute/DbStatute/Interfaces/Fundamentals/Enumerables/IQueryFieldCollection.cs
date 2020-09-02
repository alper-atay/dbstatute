using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IQueryFieldCollection : IEnumerable<QueryField>
    {
        bool HasQueryField { get; }
    }
}