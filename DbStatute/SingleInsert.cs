using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleInsert<TId, TModel> : Insert
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
    {
        protected SingleInsert(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
        }

        public TModel InsertedModel { get; private set; }
        public TModel RawModel { get; }

        public async Task InsertAsync(IDbConnection dbConnection)
        {
            if (ReadOnlyLogs.Safely)
            {
                TId insertedModelId = await InsertOperationAsync(dbConnection);
                InsertedModel = await dbConnection.QueryAsync<TModel>(insertedModelId, top: 1).ContinueWith(x => x.Result.FirstOrDefault());
            }

            if (InsertedModel is null)
            {
                OnFailed();
            }
            else
            {
                OnSucceed();
            }
        }

        protected virtual async Task<TId> InsertOperationAsync(IDbConnection dbConnection)
        {
            return (TId)await dbConnection.InsertAsync(RawModel);
        }
    }
}