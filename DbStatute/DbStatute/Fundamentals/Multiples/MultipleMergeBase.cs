using DbStatute.Enumerations;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Multiples;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Multiples
{
    public abstract class MultipleMergeBase<TModel> : MergeBase<TModel>, IMultipleMergeBase<TModel>
        where TModel : class, IModel, new()
    {
        private readonly List<TModel> _mergedModels = new List<TModel>();

        public override int MergedCount => _mergedModels.Count;
        public IEnumerable<TModel> MergedModels => MergedCount > 0 ? _mergedModels : null;
        IEnumerable<object> IMultipleMergeBase.MergedModels => MergedModels;

        public async IAsyncEnumerable<TModel> MergeAsSinglyAsync(IDbConnection dbConnection)
        {
            _mergedModels.Clear();

            await foreach (TModel mergedModel in MergeAsSinglyOperationAsync(dbConnection))
            {
                if (mergedModel is null)
                {
                    continue;
                }

                _mergedModels.Add(mergedModel);

                yield return mergedModel;
            }

            StatuteResult = MergedModels is null ? StatuteResult.Failure : StatuteResult.Success;
        }

        IAsyncEnumerable<object> IMultipleMergeBase.MergeAsSinglyAsync(IDbConnection dbConnection)
        {
            return MergeAsSinglyAsync(dbConnection);
        }

        public async Task<IEnumerable<TModel>> MergeAsync(IDbConnection dbConnection)
        {
            _mergedModels.Clear();

            IEnumerable<TModel> mergedModels = await MergeOperationAsync(dbConnection);

            if (mergedModels != null)
            {
                _mergedModels.AddRange(mergedModels);
            }

            StatuteResult = MergedModels is null ? StatuteResult.Failure : StatuteResult.Success;

            return MergedModels;
        }

        Task<IEnumerable<object>> IMultipleMergeBase.MergeAsync(IDbConnection dbConnection)
        {
            return MergeAsync(dbConnection).ContinueWith(x => x.Result.Cast<object>());
        }

        protected abstract IAsyncEnumerable<TModel> MergeAsSinglyOperationAsync(IDbConnection dbConnection);

        protected abstract Task<IEnumerable<TModel>> MergeOperationAsync(IDbConnection dbConnection);
    }
}