using DbStatute.Interfaces;
using DbStatute.Querying;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleUpdateByQuery<TId, TModel, TUpdateQuery> : UpdateByQuery<TId, TModel, TUpdateQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : UpdateQuery<TId, TModel>
    {
        private TModel _updatedModel;

        protected SingleUpdateByQuery(TUpdateQuery updateQuery) : base(updateQuery)
        {
        }

        public override int UpdatedCount => _updatedModel is null ? 0 : 1;
        public TModel UpdatedModel => (TModel)_updatedModel?.Clone();

        public async Task<TModel> UpdateAsync(IDbConnection dbConnection, TId id)
        {
            Logs.AddRange(UpdateQuery.Test());

            _updatedModel = null;

            if (ReadOnlyLogs.Safely)
            {
                _updatedModel = await UpdateOperationAsync(dbConnection, id);
            }

            return UpdatedModel;
        }

        protected virtual async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection, TId id)
        {
            IEnumerable<Field> updateFields = UpdateQuery.UpdateFields;
            TModel updateModel = UpdateQuery.UpdaterModel;
            updateModel.Id = id;
            TModel updatedModel = null;

            //We need to single update with fields
            //Update fields are will be updated columns of database table
            //updateModel is values of updated column values
            //So updateModel values change to column column values
            //updatedCount = dbConnection.UpdateAsync<TModel>(updateModel, updateFields);

            int updatedCount = 0;
            if (updatedCount > 0)
            {
                updatedModel = await dbConnection.QueryAsync<TModel>(id, top: 1)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            StatuteResult = _updatedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            // We must be default to Id. It is protect to wrong calling
            updateModel.Id = default;

            return updatedModel;
        }
    }
}