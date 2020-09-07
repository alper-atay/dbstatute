using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Singles;
using DbStatute.Proxies;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByProxy<TModel> : SingleInsertBase<TModel>, ISingleInsertByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleInsertByProxy()
        {
            InsertProxy = new InsertProxy<TModel>();
        }

        public SingleInsertByProxy(IInsertProxy<TModel> insertProxy)
        {
            InsertProxy = insertProxy ?? throw new ArgumentNullException(nameof(insertProxy));
        }

        public IInsertProxy<TModel> InsertProxy { get; }

        IInsertProxy ISingleInsertByProxy.InsertProxy => InsertProxy;

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(InsertProxy.ModelQuery.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = InsertProxy.InsertedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                return await dbConnection.InsertAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}