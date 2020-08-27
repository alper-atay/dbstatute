using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Multiples;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Multiples
{
    public abstract class MultipleUpdateBase<TModel> : UpdateBase<TModel>, IMultipleUpdateBase<TModel>
        where TModel : class, IModel, new()
    {
        private readonly List<TModel> _updatedModels = new List<TModel>();

        public int BatchSize { get; set; } = 10;
        public override int UpdatedCount => _updatedModels.Count;
        public IEnumerable<TModel> UpdatedModels => _updatedModels.Count > 0 ? _updatedModels : null;
        IEnumerable<object> IMultipleUpdateBase.UpdatedModels => UpdatedModels;

        public async IAsyncEnumerable<TModel> UpdateAsSinglyAsync(IDbConnection dbConnection)
        {
            _updatedModels.Clear();

            await foreach (TModel updatedModel in UpdateAsSinglyOperationAsync(dbConnection))
            {
                yield return updatedModel;

                _updatedModels.Add(updatedModel);
            }

            StatuteResult = UpdatedModels is null ? StatuteResult.Failure : StatuteResult.Success;
        }

        IAsyncEnumerable<object> IMultipleUpdateBase.UpdateAsSinglyAsync(IDbConnection dbConnection)
        {
            return UpdateAsSinglyAsync(dbConnection);
        }

        public async Task<IEnumerable<TModel>> UpdateAsync(IDbConnection dbConnection)
        {
            _updatedModels.Clear();

            IEnumerable<TModel> updatedModels = await UpdateOperationAsync(dbConnection);

            if (updatedModels != null)
            {
                _updatedModels.AddRange(updatedModels);
            }

            StatuteResult = UpdatedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;

            return UpdatedModels;
        }

        Task<IEnumerable<object>> IMultipleUpdateBase.UpdateAsync(IDbConnection dbConnection)
        {
            return UpdateAsync(dbConnection).ContinueWith(x => x.Result.Cast<object>());
        }

        public Task<IEnumerable<object>> UpdateByActingAsync(IDbConnection dbConnection, Action<object> action)
        {
            return UpdateByActingAsync(dbConnection, action);
        }

        protected abstract IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection);

        protected abstract Task<IEnumerable<TModel>> UpdateOperationAsync(IDbConnection dbConnection);
    }
}