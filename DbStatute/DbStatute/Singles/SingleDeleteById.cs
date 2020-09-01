using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleDeleteById<TModel> : SingleDeleteBase<TModel>, ISingleDeleteById<TModel>
        where TModel : class, IModel, new()
    {
        public SingleDeleteById(object id)
        {
            Id = id;
        }

        public object Id { get; }

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            TModel deleteModel = await dbConnection.QueryAsync<TModel>(Id, null, null, 1, Hints, Cacheable?.Key, Cacheable.ItemExpiration ?? 180, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());

            int deletedCount = 0;

            if (deleteModel is null)
            {
                deletedCount = await dbConnection.DeleteAsync<TModel>(Id, Hints, CommandTimeout, Transaction, Trace);

                if (deletedCount > 0)
                {
                    Logs.Success($"The model was not found, but the operation was successful");
                }
            }
            else
            {
                deletedCount = await dbConnection.DeleteAsync(deleteModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deletedCount > 0)
                {
                    Logs.Success("The model was found and the operation was performed successfully");
                }
            }

            return deleteModel;
        }
    }
}