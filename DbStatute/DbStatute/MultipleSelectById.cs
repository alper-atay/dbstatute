using DbStatute.Interfaces;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleSelectById<TModel> : MultipleSelect<TModel>, IMultipleSelectById<TModel>
        where TModel : class, IModel, new()
    {
        protected MultipleSelectById(IEnumerable<object> ids)
        {
            Ids = ids ?? throw new ArgumentNullException(nameof(ids));
        }

        public IEnumerable<object> Ids { get; }

        protected override async IAsyncEnumerable<TModel> SelectAsSignlyOperationAsync(IDbConnection dbConnection)
        {
            if (!ReadOnlyLogs.Safely)
            {
                yield break;
            }

            ICache cache = null;
            int cacheItemExpiration = 180;
            string cacheKey = null;

            if (Cacheable != null)
            {
                cache = Cacheable.Cache;
                cacheItemExpiration = Cacheable.ItemExpiration ?? 180;
                cacheKey = Cacheable.Key;
            }

            foreach (object id in Ids)
            {
                yield return await dbConnection.QueryAsync<TModel>(id, null, 1, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }
        }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            QueryField queryField = new QueryField(new Field(nameof(IModel.Id)), Operation.In, Ids);

            ICache cache = null;
            int cacheItemExpiration = 180;
            string cacheKey = null;

            if (Cacheable != null)
            {
                cache = Cacheable.Cache;
                cacheItemExpiration = Cacheable.ItemExpiration ?? 180;
                cacheKey = Cacheable.Key;
            }

            return await dbConnection.QueryAsync<TModel>(queryField, null, MaxSelectCount, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder);
        }
    }
}