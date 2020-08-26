using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleMerge<TModel> : Merge<TModel>, ISingleMerge<TModel>

        where TModel : class, IModel, new()
    {
        private TModel _mergedModel;

        protected SingleMerge(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
        }

        public override int MergedCount => _mergedModel is null ? 0 : 1;
        public TModel MergedModel => (TModel)_mergedModel?.Clone();
        object ISingleMerge.MergedModel => MergedModel;
        public TModel RawModel { get; }
        object ISingleMerge.RawModel => RawModel;

        public async Task<TModel> MergeAsync(IDbConnection dbConnection)
        {
            _mergedModel = null;

            if (ReadOnlyLogs.Safely)
            {
                object mergedModelId = await MergeOperationAsync(dbConnection);
                _mergedModel = await dbConnection.QueryAsync<TModel>(mergedModelId)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            StatuteResult = _mergedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return MergedModel;
        }

        Task<object> ISingleMerge.MergeAsync(IDbConnection dbConnection)
        {
            return MergeAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected virtual async Task<object> MergeOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.MergeAsync<TModel, object>(RawModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
        }
    }
}