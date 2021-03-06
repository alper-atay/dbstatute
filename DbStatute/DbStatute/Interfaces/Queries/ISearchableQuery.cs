﻿using RepoDb.Enumerations;

namespace DbStatute.Interfaces.Queries
{
    public interface ISearchableQuery
    {
        Conjunction Conjunction { get; }

        ISearchQuery SearchQuery { get; }
    }

    public interface ISearchableQuery<TModel> : ISearchableQuery
        where TModel : class, IModel, new()
    {
        new ISearchQuery<TModel> SearchQuery { get; }
    }
}