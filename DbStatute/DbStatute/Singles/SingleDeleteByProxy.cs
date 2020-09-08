using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleDeleteByProxy<TModel> : SingleDeleteBase<TModel>, ISingleDeleteByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public IDeleteProxy<TModel> DeleteProxy => throw new NotImplementedException();

        IDeleteProxy ISingleDeleteByProxy.DeleteProxy => throw new NotImplementedException();

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            if (DeleteProxy is IModelableQuery<TModel> modelableQuery)
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

            if (DeleteProxy is ISearchableQuery<TModel> searchableModel)
            {
                Logs.AddRange(searchableModel.SearchQuery.Build(searchableModel.Conjunction, out QueryGroup queryGroup));

                if (ReadOnlyLogs.Safely)
                {
                    IEnumerable<Field> fields = null;
                    IEnumerable<OrderField> orderFields = null;

                    if (DeleteProxy is IFieldableQuery<TModel> fieldableQuery)
                    {
                        bool fieldsBuilt = fieldableQuery.FieldQuery.Fields.Build(out fields);

                        if (!fieldsBuilt)
                        {
                            fields = null;
                        }
                    }

                    if (DeleteProxy is IOrderFieldableQuery<TModel> orderFieldableQuery)
                    {
                        bool orderFieldsBuilt = orderFieldableQuery.OrderFieldQuery.OrderFields.Build(out orderFields);

                        if (!orderFieldsBuilt)
                        {
                            orderFields = null;
                        }
                    }

                    TModel selectedModel = await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());

                    int deletedCount = await dbConnection.DeleteAsync(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (deletedCount > 0)
                    {
                        return selectedModel;
                    }
                }
            }

            return null;
        }
    }
}