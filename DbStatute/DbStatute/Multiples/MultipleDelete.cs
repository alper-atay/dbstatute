using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
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
        public MultipleDelete(IEnumerable<TModel> sourceModels)
        {
            SourceModels = sourceModels ?? throw new ArgumentNullException(nameof(sourceModels));
        }

        public IEnumerable<TModel> SourceModels { get; }

        IEnumerable<object> ISourceableModelsQuery.SourceModels => SourceModels;

        protected override async IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel selectedModel in SourceModels)
            {
                int deletedCount = await dbConnection.DeleteAsync(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deletedCount > 0)
                {
                    yield return selectedModel;
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection)
        {
            int selectedCount = SourceModels.Count();

            if (selectedCount > 0)
            {
                int deletedCount = await dbConnection.DeleteAllAsync(SourceModels, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deletedCount > 0)
                {
                    if (deletedCount != selectedCount)
                    {
                        Logs.Warning($"{selectedCount} models selected and {deletedCount} models deleted");
                    }

                    return SourceModels;
                }
            }

            return null;
        }
    }
}