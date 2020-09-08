using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Singles;
using DbStatute.Proxies;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdateByProxy<TModel> : SingleUpdateBase<TModel>, ISingleUpdateByProxy
        where TModel : class, IModel, new()
    {
        public SingleUpdateByProxy()
        {
            UpdateProxy = new UpdateProxy<TModel>();
        }

        public SingleUpdateByProxy(IUpdateProxy<TModel> updateProxy)
        {
            UpdateProxy = updateProxy ?? throw new ArgumentNullException(nameof(updateProxy));
        }

        public IUpdateProxy<TModel> UpdateProxy { get; }

        IUpdateProxy ISingleUpdateByProxy.UpdateProxy => UpdateProxy;

        protected override async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {

            return null;
        }
    }
}