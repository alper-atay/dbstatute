using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IOrderFields
    {
        bool HasOrderField { get; }

        IEnumerable<OrderField> OrderFields { get; }
    }
}