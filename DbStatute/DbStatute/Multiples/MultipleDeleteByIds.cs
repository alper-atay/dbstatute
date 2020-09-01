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

        protected override async IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection, bool allowNullReturnIfDeleted = false)
        {
            foreach (object id in Ids)
            {
                TModel deletedModel = await dbConnection.QueryAsync<TModel>(id, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
                    .ContinueWith(x => x.Result.FirstOrDefault());

                int deletedCount = await dbConnection.DeleteAsync<TModel>(id, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deletedCount > 0 && !(deletedModel is null))
                {
                    yield return deletedModel;
                }
                else if (deletedCount > 0 && deletedModel is null)
                {
                    if (allowNullReturnIfDeleted)
                    {
                        yield return null;
                    }
                    else
                    {
                        yield return new TModel() { Id = id };
                    }
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection)
        {
            QueryField queryField = new QueryField("Id", Operation.In, Ids);
            IEnumerable<TModel> deletedModels = await dbConnection.QueryAsync<TModel>(queryField, null, null, null, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

            int deletedCount = await dbConnection.DeleteAsync<TModel>(queryField, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            return deletedCount > 0 ? deletedModels : null;
        }
    }
}