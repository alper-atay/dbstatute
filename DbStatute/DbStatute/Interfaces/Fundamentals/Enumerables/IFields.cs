using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IFields
    {
        IEnumerable<Field> Fields { get; }

        bool HasField { get; }
    }
}