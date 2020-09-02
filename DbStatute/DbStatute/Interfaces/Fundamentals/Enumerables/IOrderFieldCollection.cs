using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IOrderFieldCollection : IEnumerable<OrderField>
    {
        bool HasOrderField { get; }
    }
}