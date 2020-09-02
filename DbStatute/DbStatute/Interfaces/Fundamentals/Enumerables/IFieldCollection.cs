using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IFieldCollection : IReadOnlyCollection<Field>
    {
        public bool HasField { get; }
    }
}