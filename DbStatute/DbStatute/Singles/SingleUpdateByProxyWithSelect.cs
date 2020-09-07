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
    public class SingleUpdateByProxyWithSelect<TModel> : SingleUpdateBase<TModel>, ISingleUpdateByProxyWithSelect<TModel>
        where TModel : class, IModel, new()
    {
        public SingleUpdateByProxyWithSelect(IUpdateProxyWithSelect<TModel> updateProxyWithSelect)
        {
            UpdateProxyWithSelect = updateProxyWithSelect;
        }

        public IUpdateProxyWithSelect<TModel> UpdateProxyWithSelect { get; }

        IUpdateProxyWithSelect ISingleUpdateByProxyWithSelect.UpdateProxyWithSelect => UpdateProxyWithSelect;

        protected override Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            dbConnection.UpdateAsync<TModel>()

            throw new NotImplementedException();
        }
    }
}