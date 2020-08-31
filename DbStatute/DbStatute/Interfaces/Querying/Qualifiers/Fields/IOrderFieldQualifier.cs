﻿using DbStatute.Interfaces.Utilities;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Qualifiers.Fields
{
    public interface IOrderFieldQualifier : ISettableOrderField
    {
        bool HasOrderField { get; }
        IEnumerable<OrderField> ReadOnlyOrderFields { get; }
    }

    public interface IOrderFieldQualifier<TModel> : ISettableOrderField<TModel>, IOrderFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}