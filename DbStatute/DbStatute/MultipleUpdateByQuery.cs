using DbStatute.Interfaces;
using DbStatute.Querying;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    /// <summary>
    /// TODO:
    /// Atomic updates
    /// UpdateAsAtomic(IDbConnection dbConnection, IEnumerable<TId> ids)
    /// For performance gain
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TUpdateQuery"></typeparam>

    public abstract class MultipleUpdateByQuery<TId, TModel, TUpdateQuery> : UpdateByQuery<TId, TModel, TUpdateQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : UpdateQuery<TId, TModel>
    {
        /// <summary>
        /// Huge update create problems ?
        /// Just collect updated ids?
        /// </summary>
        private readonly List<TId> _updatedModelIds = new List<TId>();

        protected MultipleUpdateByQuery(TUpdateQuery updateQuery) : base(updateQuery)
        {
        }

        public bool AllowNullModelOnEmission { get; set; } = false;
        public override int UpdatedCount => _updatedModelIds.Count;
        public IEnumerable<TId> UpdatedModelIds => UpdatedCount > 0 ? _updatedModelIds : null;

        public async Task UpdateAsSingularAsync(IDbConnection dbConnection, IEnumerable<TId> ids)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            _updatedModelIds.Clear();

            Logs.Clear();
            Logs.AddRange(UpdateQuery.Test());

            if (ids.Count() > 0 && ReadOnlyLogs.Safely)
            {
                _updatedModelIds.AddRange(await UpdateAsSingularOperationAsync(dbConnection, ids));
            }
        }

        // Need a cool name :D
        // SingularEmission not a good name xD
        // Purpose every updated model invoke a action
        // Maybe every update need to some changes on application
        // Relations need to synchronization
        // (rhyme)
        public async Task UpdateAsSingularEmissionAsync(IDbConnection dbConnection, IEnumerable<TId> ids, Action<TModel> action)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            _updatedModelIds.Clear();

            Logs.Clear();
            Logs.AddRange(UpdateQuery.Test());

            await foreach (TModel updatedModel in UpdateAsSingularEmissionOperationAsync(dbConnection, ids))
            {
                // I think native method not return null value
                // Maybe override method return null value
                if (updatedModel != null || (updatedModel is null && AllowNullModelOnEmission))
                {
                    action.Invoke(updatedModel);
                }

                if (updatedModel != null)
                {
                    _updatedModelIds.Add(updatedModel.Id);
                }
            }
        }

        protected async IAsyncEnumerable<TModel> UpdateAsSingularEmissionOperationAsync(IDbConnection dbConnection, IEnumerable<TId> ids)
        {
            IEnumerable<Field> updateFields = UpdateQuery.UpdateFields;
            TModel updateModel = UpdateQuery.UpdaterModel;

            foreach (TId id in ids)
            {
                updateModel.Id = id;

                //We need to single update with fields
                //Update fields are will be updated columns of database table
                //updateModel is values of updated column values
                //So updateModel values change to column values
                //updatedCount = dbConnection.UpdateAsync<TModel>(updateModel, updateFields);

                int updatedCount = 0;
                if (updatedCount > 0)
                {
                    TModel updatedModel = await dbConnection.QueryAsync<TModel>(id, top: 1)
                        .ContinueWith(x => x.Result.FirstOrDefault());

                    if (updatedModel is null)
                    {
                        continue;
                    }

                    yield return updatedModel;
                }
            }
        }

        protected virtual async Task<IEnumerable<TId>> UpdateAsSingularOperationAsync(IDbConnection dbConnection, IEnumerable<TId> ids)
        {
            ICollection<TId> updatedModelIds = new Collection<TId>();

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
                    TModel updatedModel = await dbConnection.QueryAsync<TModel>(id, top: 1)
                        .ContinueWith(x => x.Result.FirstOrDefault());

                    if (updatedModel is null)
                    {
                        continue;
                    }

                    updatedModelIds.Add(updatedModel.Id);
                }
            }

            return updatedModelIds;
        }
    }
}