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
    public class MultipleSelectByIds<TModel> : MultipleSelectBase<TModel>, IMultipleSelectByIds<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleSelectByIds(IEnumerable<object> ids)
        {
            Ids = ids ?? throw new ArgumentNullException(nameof(ids));
        }

        public IEnumerable<object> Ids { get; }

        protected override async IAsyncEnumerable<TModel> SelectAsSignlyOperationAsync(IDbConnection dbConnection)
        {
            foreach (object id in Ids)
            {
                TModel selectedModel = await dbConnection.QueryAsync<TModel>(id, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
                    .ContinueWith(x => x.Result.FirstOrDefault());

                if (selectedModel is null)
                {
                    continue;
                }

                yield return selectedModel;
            }
        }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            QueryField queryField = new QueryField("Id", Operation.In, Ids);

            return await dbConnection.QueryAsync<TModel>(queryField, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);
        }
    }
}