using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdate<TModel> : SingleUpdateBase<TModel>, ISingleUpdate<TModel>
        where TModel : class, IModel, new()
    {
        public TModel ReadyModel { get; }
        object IReadyModel.ReadyModel => ReadyModel;

        protected override Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}