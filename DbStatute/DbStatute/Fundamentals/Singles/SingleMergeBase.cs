using DbStatute.Enumerations;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Singles;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Singles
{
    public abstract class SingleMergeBase<TModel> : MergeBase<TModel>, ISingleMergeBase<TModel>
        where TModel : class, IModel, new()
    {
        private TModel _mergedModel;

        public override int MergedCount => MergedModel is null ? 0 : 1;
        public TModel MergedModel => _mergedModel;
        object ISingleMergeBase.MergedModel => MergedModel;

        public async Task<TModel> MergeAsync(IDbConnection dbConnection)
        {
            _mergedModel = await MergeOperationAsync(dbConnection);

            StatuteResult = _mergedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return MergedModel;
        }

        Task<object> ISingleMergeBase.MergeAsync(IDbConnection dbConnection)
        {
            return MergeAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected abstract Task<TModel> MergeOperationAsync(IDbConnection dbConnection);
    }
}