﻿using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Fundamentals.Maps
{
    public interface IReadOnlyFieldOperationMap
    {
        IReadOnlyDictionary<Field, Operation> ReadOnlyFieldOperationMap { get; }
    }
}