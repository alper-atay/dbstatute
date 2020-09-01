using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleDelete<TModel> : MultipleDeleteBase<TModel>, IMultipleDelete<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleDelete(IEnumerable<TModel> readyModels)
        {
            ReadyModels = readyModels ?? throw new ArgumentNullException(nameof(readyModels));
        }

        public IEnumerable<TModel> ReadyModels { get; }
        IEnumerable<object> IReadyModels.ReadyModels => ReadyModels;

        protected override async IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection, bool allowNullReturnIfDeleted = false)
        {
            foreach (TModel deletedModel in ReadyModels)
            {
                if (deletedModel is null)
                {
                    continue;
                }

                int deletedCount = await dbConnection.DeleteAsync(deletedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deletedCount > 0 && !(deletedModel is null))
                {
                    yield return deletedModel;
                }
                else if (deletedCount > 0 && deletedModel is null && allowNullReturnIfDeleted)
                {
                    yield return null;
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection)
        {
            int deletedCount = await dbConnection.DeleteAllAsync(ReadyModels, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            if (deletedCount > 0)
            {
                int readyModelCount = ReadyModels.Count();

                if (deletedCount != readyModelCount)
                {
                    Logs.Warning($"{readyModelCount} models inputted and {deletedCount} models deleted");
                }

                return ReadyModels;
            }

            return null;
        }
    }
}