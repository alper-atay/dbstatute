﻿using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleMerge<TModel> : SingleMergeBase<TModel>, ISingleMerge<TModel>
        where TModel : class, IModel, new()
    {
        public SingleMerge(TModel sourceModel)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public TModel SourceModel { get; }

        object ISourceableQuery.SourceModel => SourceModel;

        protected override async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.MergeAsync(SourceModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder).ContinueWith(x => (TModel)x.Result);
        }
    }
}