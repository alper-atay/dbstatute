using DbStatute.Interfaces;
using RepoDb;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleSelectById<TModel> : SingleSelect<TModel>, ISingleSelectById<TModel>

        where TModel : class, IModel, new()
    {
        protected SingleSelectById(object id)
        {
            Id = id;
        }

        public object Id { get; }

        protected override async Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            return await dbConnection.QueryAsync<TModel>(Id)
                .ContinueWith(x => x.Result.FirstOrDefault());
        }
    }
}