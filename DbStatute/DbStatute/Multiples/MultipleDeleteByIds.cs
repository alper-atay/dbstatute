using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleDeleteByIds<TModel> : MultipleDeleteBase<TModel>, IMultipleDeleteByIds<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleDeleteByIds(IEnumerable<object> ids)
        {
            Ids = ids ?? throw new ArgumentNullException(nameof(ids));
        }

        public IEnumerable<object> Ids { get; }

        protected override async IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            int idCount = Ids.Count();

            if (idCount > 0)
            {
                foreach (object id in Ids)
                {
                    TModel selectedModel = await dbConnection.QueryAsync<TModel>(id, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
                        .ContinueWith(x => x.Result.FirstOrDefault());

                    if (selectedModel is null)
                    {
                        continue;
                    }

                    int deletedCount = await dbConnection.DeleteAsync<TModel>(id, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (deletedCount > 0)
                    {
                        yield return selectedModel;
                    }
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection)
        {
            var idCount = Ids.Count();

            if (idCount > 0)
            {
                QueryField idsInQuery = new QueryField(nameof(IModel.Id), Operation.In, Ids);

                IEnumerable<TModel> selectedModels = await dbConnection.QueryAsync<TModel>(idsInQuery, null, null, null, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                int selectedCount = selectedModels.Count();

                if (selectedCount > 0)
                {
                    int deletedCount = await dbConnection.DeleteAsync<TModel>(idsInQuery, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (deletedCount != selectedCount)
                    {
                        Logs.Warning($"{selectedCount} models selected and {deletedCount} models deleted");
                    }

                    if (deletedCount > 0)
                    {
                        return selectedModels;
                    }
                }
            }

            return null;
        }
    }
}