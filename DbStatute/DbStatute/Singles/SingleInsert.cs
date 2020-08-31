using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsert<TModel> : SingleInsertBase<TModel>, ISingleInsert<TModel>
        where TModel : class, IModel, new()
    {
        public SingleInsert(TModel rawModel)
        {
            RawModel = rawModel;
        }

        public TModel RawModel { get; }
        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.InsertAsync<TModel, TModel>(RawModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
        }
    }
}