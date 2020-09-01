using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleMerge<TModel> : SingleMergeBase<TModel>, ISingleMerge<TModel>
        where TModel : class, IModel, new()
    {
        public TModel RawModel { get; }
        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.MergeAsync(RawModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder).ContinueWith(x => (TModel)x.Result);
        }
    }
}