using Basiclog;
using RepoDb;
using System;
using System.Collections.Generic;

namespace DbStatute.Interfaces
{
    public interface IOptionalUpdate<TId> : IModel<TId>, IReadOnlyLogbookTestable
        where TId : struct, IConvertible
    {
        public IEnumerable<Field> Fields { get; }
        public bool HasField { get; }
    }
}