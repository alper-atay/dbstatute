using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleSelectById<TModel> : SingleSelectBase<TModel>, ISingleSelectById<TModel>
        where TModel : class, IModel, new()
    {
        public SingleSelectById(object id)
        {
            Id = id;
        }

        public object Id { get; }

        protected override async Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.QueryAsync<TModel>(Id, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
                .ContinueWith(x => x.Result.FirstOrDefault());
        }
    }
}