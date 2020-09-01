﻿using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
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
        public SingleMerge(TModel readyModel)
        {
            ReadyModel = readyModel ?? throw new ArgumentNullException(nameof(readyModel));
        }

        public TModel ReadyModel { get; }
        object IReadyModel.ReadyModel => ReadyModel;

        protected override async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.MergeAsync(ReadyModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder)
                .ContinueWith(x => (TModel)x.Result);
        }
    }
}