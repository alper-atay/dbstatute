using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
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
            TModel insertModel = null;

            {
                if (InsertProxy is ISourceableQuery<TModel> query)
                {
                    ThrowIfNotNull(insertModel, nameof(insertModel));

                    insertModel = GetSourceableModelQueryResult(query);
                }
            }

            {
                if (InsertProxy is IModelableQuery<TModel> query)
                {
                    ThrowIfNotNull(insertModel, nameof(insertModel));

                    insertModel = GetModelableQueryResult(query);
                }
            }

            if (!(insertModel is null))
            {
                IEnumerable<Field> fields = InsertProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;

                return await dbConnection.InsertAsync<TModel, TModel>(insertModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}