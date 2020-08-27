using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Multiples
{
    public abstract class MultipleDeleteBase<TModel> : DeleteBase<TModel>, IMultipleDeleteBase<TModel>
        where TModel : class, IModel, new()
    {
        private readonly List<TModel> _deletedModels = new List<TModel>();

        public override int DeletedCount => _deletedModels.Count;
        public IEnumerable<TModel> DeletedModels => DeletedCount > 0 ? _deletedModels : null;
        IEnumerable<object> IMultipleDeleteBase.DeletedModels => DeletedModels;

        public async IAsyncEnumerable<TModel> DeleteAsSinglyAsync(IDbConnection dbConnection)
        {
            await foreach (TModel deletedModel in DeleteAsSinglyOperationAsync(dbConnection))
            {
                if (deletedModel is null)
                {
                    continue;
                }

                _deletedModels.Add(deletedModel);

                yield return deletedModel;
            }

            StatuteResult = DeletedModels is null ? StatuteResult.Failure : StatuteResult.Success;
        }

        IAsyncEnumerable<object> IMultipleDeleteBase.DeleteAsSinglyAsync(IDbConnection dbConnection)
        {
            return DeleteAsSinglyAsync(dbConnection);
        }

        public async Task<IEnumerable<TModel>> DeleteAsync(IDbConnection dbConnection)
        {
            _deletedModels.Clear();

            IEnumerable<TModel> deletedModels = await DeleteOperationAsync(dbConnection);

            if (deletedModels != null)
            {
                _deletedModels.AddRange(await DeleteOperationAsync(dbConnection));
            }

            StatuteResult = DeletedModels is null ? StatuteResult.Failure : StatuteResult.Success;
            return DeletedModels;
        }

        Task<IEnumerable<object>> IMultipleDeleteBase.DeleteAsync(IDbConnection dbConnection)
        {
            return DeleteAsync(dbConnection).ContinueWith(x => x.Result.Cast<object>());
        }

        protected abstract IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection);

        protected abstract Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection);
    }
}