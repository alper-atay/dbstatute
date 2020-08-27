using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByRawModel<TModel> : SingleInsertBase<TModel>, ISingleInsertByRawModel<TModel>
        where TModel : class, IModel, new()
    {
        public TModel RawModel { get; }

        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.InsertAsync<TModel, TModel>(RawModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
        }
    }
}