﻿using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using RepoDb.Enumerations;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface ISelectQueryGroupBuilder : IQueryGroupBuilder
    {
        Conjunction Conjunction { get; set; }
        IOperationFieldQualifier OperationFieldQualifier { get; }
    }

    public interface ISelectQueryGroupBuilder<TModel> : IQueryGroupBuilder<TModel>, ISelectQueryGroupBuilder
        where TModel : class, IModel, new()
    {
        new IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }
    }
}