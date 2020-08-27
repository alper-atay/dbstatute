﻿using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleDeleteById<TModel> : SingleDeleteBase<TModel>, ISingleDeleteById<TModel>
        where TModel : class, IModel, new()
    {
        public SingleDeleteById(object id)
        {
            Id = id;
        }

        public object Id { get; }

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            TModel deleteModel = await dbConnection.QueryAsync<TModel>(Id, null, 1, Hints, Cacheable?.Key, Cacheable.ItemExpiration ?? 180, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());

            if (deleteModel != null)
            {
                int deletedCount = await dbConnection.DeleteAsync(deleteModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return deleteModel;
        }
    }
}