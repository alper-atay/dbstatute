using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Querying;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleInsertByProxy<TModel> : MultipleInsertBase<TModel>, IMultipleInsertByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleInsertByProxy()
        {
            InsertProxy = new InsertProxy<TModel>();
        }

        public MultipleInsertByProxy(IInsertProxy<TModel> insertProxy)
        {
            InsertProxy = insertProxy ?? throw new ArgumentNullException(nameof(insertProxy));
        }

        public IInsertProxy<TModel> InsertProxy { get; }

        IInsertProxy IMultipleInsertByProxy.InsertProxy => InsertProxy;

        protected override async IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
        {
            if (!InsertProxy.InsertCount.HasValue)
            {
                yield break;
            }

            int insertCount = InsertProxy.InsertCount.Value;

            if (insertCount < 1)
            {
                yield break;
            }

            Logs.AddRange(InsertProxy.ModelQualifierGroup.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = InsertProxy.InsertedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                for (int i = 0; i < insertCount; i++)
                {
                    TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (!(insertedModel is null))
                    {
                        yield return insertedModel;
                    }
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            if (!InsertProxy.InsertCount.HasValue)
            {
                return null;
            }

            int insertCount = InsertProxy.InsertCount.Value;

            if (insertCount < 1)
            {
                return null;
            }

            Logs.AddRange(InsertProxy.ModelQualifierGroup.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = InsertProxy.InsertedFieldQualifier.Build<TModel>(out IEnumerable<RepoDb.Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                ICollection<TModel> insertedModels = new Collection<TModel>();

                for (int i = 0; i < insertCount; i++)
                {
                    TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (insertedModel is null)
                    {
                        continue;
                    }

                    insertedModels.Add(insertedModel);
                }

                if (insertedModels.Count > 0)
                {
                    return insertedModels;
                }
            }

            return null;
        }
    }
}