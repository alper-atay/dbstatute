using DbStatute.Extensions;
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
    public class SingleUpdateByProxy<TModel> : SingleUpdateBase<TModel>, ISingleUpdateByProxy<TModel>
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


            if (UpdateProxy is ISearchableQuery<TModel> searchableQuery)
            {
                Logs.AddRange(searchableQuery.SearchQuery.Build(searchableQuery.Conjunction, out QueryGroup queryGroup));

                if(ReadOnlyLogs.Safely)
                {
                    if (UpdateProxy is IFieldableQuery<TModel> fieldableQuery)
                    {
                        bool fieldsBuilt = fieldableQuery.FieldQuery.Fields.Build(out IEnumerable<Field> fields);

                        if (!fieldsBuilt)
                        {
                            fields = null;
                        }
                    }



                }


            }





            if (UpdateProxy is ISourceableModelQuery<TModel> sourceableModelQuery)
            {
                TModel sourceModel = sourceableModelQuery.SourceModel;
            }

            if (UpdateProxy is IModelableQuery<TModel> modelableQuery)
            {
                Logs.AddRange(modelableQuery.ModelQuery.Build(out TModel model));

                if (ReadOnlyLogs.Safely)
                {
                    int deletedCount = await dbConnection.DeleteAsync(model, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (deletedCount > 0)
                    {
                        return model;
                    }
                }
            }

            return null;
        }
    }
}