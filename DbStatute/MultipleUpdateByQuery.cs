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
    public abstract class MultipleUpdateByQuery<TId, TModel, TUpdateQuery> : UpdateByQuery<TId, TModel, TUpdateQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : UpdateQuery<TId, TModel>
    {
        /// <summary>
        /// Huge update create problems ?
        /// Just collect updated ids?
        /// </summary>
        private IList<TId> _updatedModelIds = new List<TId>();

        protected MultipleUpdateByQuery(TUpdateQuery updateQuery) : base(updateQuery)
        {
        }

        public override int UpdatedCount => _updatedModelIds.Count;
        public IEnumerable<TId> UpdatedModelIds => _updatedModelIds;

        public async Task<IEnumerable<TModel>> UpdateAsync(IEnumerable<TId> ids, IDbConnection dbConnection)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            _updatedModelIds.Clear();
            Logs.AddRange(UpdateQuery.Test());

            if (ReadOnlyLogs.Safely)
            {
                foreach (TId id in ids)
                {
                    IEnumerable<Field> updateFields = UpdateQuery.UpdateFields;
                    TModel updateModel = UpdateQuery.UpdaterModel;
                    updateModel.Id = id;

                    //We need to single update with fields
                    //Update fields are will be updated columns of database table
                    //updateModel is values of updated column values
                    //So updateModel values change to column column values
                    //updatedCount = dbConnection.UpdateAsync<TModel>(updateModel, updateFields);

                    int updatedCount = 0;
                    if (updatedCount > 0)
                    {
                        TModel updatedModel = await dbConnection.QueryAsync<TModel>(id, top: 1).ContinueWith(x => x.Result.FirstOrDefault());
                        if (updatedModel is null)
                        {
                            continue;
                        }

                        _updatedModelIds.Add(updatedModel.Id);
                    }

                    // We must be default to Id. It is protect to wrong calling
                    updateModel.Id = default;
                }
            }

            return null;
        }
    }
}