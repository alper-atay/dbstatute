using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleSelectById<TId, TModel> : SingleSelect<TId, TModel>, ISingleSelectById<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        protected SingleSelectById(TId id)
        {
            Id = id;
        }

        public TId Id { get; }

        protected override async Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            if (ReadOnlyLogs.Safely)
            {
                return await dbConnection.QueryAsync<TModel>(Id)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            return null;
        }
    }
}