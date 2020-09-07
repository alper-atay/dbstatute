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
    public class SingleDeleteByProxy<TModel> : SingleDeleteBase<TModel>, ISingleDeleteByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleDeleteByProxy()
        {
            DeleteProxy = new DeleteProxy<TModel>();
        }

        public SingleDeleteByProxy(IDeleteProxy<TModel> deleteProxy)
        {
            DeleteProxy = deleteProxy ?? throw new ArgumentNullException(nameof(deleteProxy));
        }

        public IDeleteProxy<TModel> DeleteProxy { get; }

        IDeleteProxy ISingleDeleteByProxy.DeleteProxy => DeleteProxy;

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            SingleSelectByProxy<TModel> singleSelectByProxy = new SingleSelectByProxy<TModel>(DeleteProxy.SelectProxy)
            {
                Hints = Hints,
                CommandTimeout = CommandTimeout,
                Transaction = Transaction,
                Trace = Trace,
                StatementBuilder = StatementBuilder,
                Cacheable = Cacheable
            };

            await singleSelectByProxy.SelectAsync(dbConnection);

            Logs.AddRange(singleSelectByProxy.ReadOnlyLogs);

            if (ReadOnlyLogs.Safely)
            {
                TModel selectedModel = singleSelectByProxy.SelectedModel;

                int deletedCount = await dbConnection.DeleteAsync<TModel>(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deletedCount > 0)
                {
                    return selectedModel;
                }
            }

            return null;
        }
    }
}