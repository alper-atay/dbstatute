using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsert<TModel> : SingleInsertBase<TModel>, ISingleInsert<TModel>
        where TModel : class, IModel, new()
    {
        public SingleInsert(TModel readyModel)
        {
            ReadyModel = readyModel ?? throw new ArgumentNullException(nameof(readyModel));
        }

        public TModel ReadyModel { get; }

        object IReadyModel.ReadyModel => ReadyModel;

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.InsertAsync<TModel, TModel>(ReadyModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
        }
    }
}