using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying
{
    public interface ISortableQuery
    {
        public bool HasOrderField { get; }
        public IEnumerable<OrderField> OrderFields { get; }
    }
}