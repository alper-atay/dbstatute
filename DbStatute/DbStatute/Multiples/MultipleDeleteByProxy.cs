using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Proxies;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleDeleteByProxy<TModel> : MultipleDeleteBase<TModel>, IMultipleDeleteByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleDeleteByProxy()
        {
            DeleteProxy = new DeleteProxy<TModel>();
        }

        public MultipleDeleteByProxy(IDeleteProxy<TModel> deleteProxy)
        {
            DeleteProxy = deleteProxy ?? throw new ArgumentNullException(nameof(deleteProxy));
        }

        public IDeleteProxy<TModel> DeleteProxy { get; }

        IDeleteProxy IMultipleDeleteByProxy.DeleteProxy => DeleteProxy;

        protected override async IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(DeleteProxy.SelectProxy.WhereQuery.Build<TModel>(Conjunction.And, out QueryGroup queryGroup));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = DeleteProxy.SelectProxy.SelectedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);
                bool orderFieldsBuilt = DeleteProxy.SelectProxy.OrderFieldQualifier.Build<TModel>(out IEnumerable<OrderField> orderFields);

                if (fieldsBuilt)
                {
                    fields = null;
                }

                if (orderFieldsBuilt)
                {
                    orderFields = null;
                }

                IEnumerable<TModel> selectedModels = await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, null, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                int selectedCount = selectedModels.Count();

                if (selectedCount > 0)
                {
                    int selectedDeleteCount = 0;

                    foreach (TModel selectedModel in selectedModels)
                    {
                        int deletedCount = await dbConnection.DeleteAsync(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                        selectedDeleteCount += deletedCount;

                        if (deletedCount > 0)
                        {
                            yield return selectedModel;
                        }
                    }

                    if (selectedDeleteCount != selectedCount)
                    {
                        Logs.Warning($"{selectedCount} models selected and {selectedDeleteCount} models deleted");
                    }
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(DeleteProxy.SelectProxy.WhereQuery.Build<TModel>(Conjunction.And, out QueryGroup queryGroup));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = DeleteProxy.SelectProxy.SelectedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);
                bool orderFieldsBuilt = DeleteProxy.SelectProxy.OrderFieldQualifier.Build<TModel>(out IEnumerable<OrderField> orderFields);

                if (fieldsBuilt)
                {
                    fields = null;
                }

                if (orderFieldsBuilt)
                {
                    orderFields = null;
                }

                IEnumerable<TModel> selectedModels = await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, null, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                int selectedCount = selectedModels.Count();

                if (selectedCount > 0)
                {
                    int deletedCount = await dbConnection.DeleteAllAsync(selectedModels, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (deletedCount != selectedCount)
                    {
                        Logs.Warning($"{selectedCount} models selected and {deletedCount} models deleted");
                    }

                    if (deletedCount > 0)
                    {
                        return selectedModels;
                    }
                }
            }

            return null;
        }
    }
}