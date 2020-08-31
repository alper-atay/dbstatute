using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByQuery<TModel, TInsertProxy> : SingleInsertBase<TModel>, ISingleInsertByProxy<TModel, TInsertProxy>
        where TModel : class, IModel, new()
        where TInsertProxy : IInsertProxy<TModel>
    {
        public SingleInsertByQuery(TInsertProxy insertProxy)
        {
            InsertProxy = insertProxy ?? throw new ArgumentNullException(nameof(insertProxy));
        }

        public TInsertProxy InsertProxy { get; }

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.InsertAsync<TModel, TModel>((TModel)null/*insertModel*/, null/*fields*/, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
        }
    }
}