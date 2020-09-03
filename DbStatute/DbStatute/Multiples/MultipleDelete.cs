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

        protected override async IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            int selectedCount = ReadyModels.Count();

            if (selectedCount > 0)
            {
                foreach (TModel selectedModel in ReadyModels)
                {
                    int deletedCount = await dbConnection.DeleteAsync(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (deletedCount > 0)
                    {
                        yield return selectedModel;
                    }
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection)
        {
            int selectedCount = ReadyModels.Count();

            if (selectedCount > 0)
            {
                int deletedCount = await dbConnection.DeleteAllAsync(ReadyModels, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deletedCount > 0)
                {
                    if (deletedCount != selectedCount)
                    {
                        Logs.Warning($"{selectedCount} models selected and {deletedCount} models deleted");
                    }

                    return ReadyModels;
                }
            }

            return null;
        }
    }
}