using Basiclog;
using RepoDb;
using System;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying
{
    public interface IUpdateQuery : IReadOnlyLogbookTestable
    {
        IEnumerable<Field> UpdateFields { get; }
    }
}