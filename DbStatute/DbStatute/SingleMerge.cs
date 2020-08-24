using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleMerge<TId, TModel> : Merge, ISingleMerge<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private TModel _mergedModel;

        protected SingleMerge(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
        }

        public override int MergedCount => _mergedModel is null ? 0 : 1;
        public TModel MergedModel => (TModel)_mergedModel?.Clone();
        public TModel RawModel { get; }

        public async Task<TModel> MergeAsync(IDbConnection dbConnection)
        {
            _mergedModel = null;

            if (ReadOnlyLogs.Safely)
            {
                TId mergedModelId = await MergeOperationAsync(dbConnection);
                _mergedModel = await dbConnection.QueryAsync<TModel>(mergedModelId)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            return MergedModel;
        }

        protected virtual async Task<TId> MergeOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.MergeAsync<TModel, TId>(RawModel, Hints, CommandTimeout, DbTransaction, Trace, StatementBuilder);
        }
    }
}