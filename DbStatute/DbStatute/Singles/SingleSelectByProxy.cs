using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Singles;
using DbStatute.Querying;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleSelectByProxy<TModel> : SingleSelectBase<TModel>, ISingleSelectByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleSelectByProxy()
        {
            SelectProxy = new SelectProxy<TModel>();
        }

        public SingleSelectByProxy(ISelectProxy<TModel> selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public ISelectProxy<TModel> SelectProxy { get; }
        ISelectProxy ISingleSelectByProxy.SelectProxy => SelectProxy;

        protected override Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            bool fieldQualifierEnabled = SelectProxy.FieldQualifierEnabled;
            bool orderFieldEnabled = SelectProxy.OrderFieldQualifierEnabled;


            return default;
        }
    }
}