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

            if (!ReadOnlyLogs.Safely)
            {
                yield break;
            }

            await foreach (TModel updatedModel in UpdateAsSinglyOperationAsync(dbConnection))
            {
                yield return updatedModel;

                _updatedModels.Add(updatedModel);
            }

            StatuteResult = UpdatedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;
        }

        IAsyncEnumerable<object> IMultipleUpdateBase.UpdateAsSinglyAsync(IDbConnection dbConnection)
        {
            return UpdateAsSinglyAsync(dbConnection);
        }

        public async Task<IEnumerable<TModel>> UpdateAsync(IDbConnection dbConnection)
        {
            _updatedModels.Clear();

            if (ReadOnlyLogs.Safely)
            {
                _updatedModels.AddRange(await UpdateOperationAsync(dbConnection));
            }

            StatuteResult = UpdatedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;

            return UpdatedModels;
        }

        Task<IEnumerable<object>> IMultipleUpdateBase.UpdateAsync(IDbConnection dbConnection)
        {
            return UpdateAsync(dbConnection).ContinueWith(x => x.Result.Cast<object>());
        }

        public async Task<IEnumerable<TModel>> UpdateByActingAsync(IDbConnection dbConnection, Action<TModel> action)
        {
            _updatedModels.Clear();

            if (ReadOnlyLogs.Safely)
            {
                _updatedModels.AddRange(await UpdateByActingOperationAsync(dbConnection, action));
            }

            StatuteResult = UpdatedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;

            return UpdatedModels;
        }

        public Task<IEnumerable<object>> UpdateByActingAsync(IDbConnection dbConnection, Action<object> action)
        {
            return UpdateByActingAsync(dbConnection, action);
        }

        protected abstract IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection);

        protected abstract Task<IEnumerable<TModel>> UpdateByActingOperationAsync(IDbConnection dbConnection, Action<TModel> action);

        protected abstract Task<IEnumerable<TModel>> UpdateOperationAsync(IDbConnection dbConnection);
    }
}