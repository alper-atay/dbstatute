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
    public class MultipleInsertByProxy<TModel> : MultipleInsertBase<TModel>, IMultipleInsertByProxyOnReadyModels<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleInsertByProxy(IEnumerable<TModel> readyModels)
        {
            ReadyModels = readyModels ?? throw new ArgumentNullException(nameof(readyModels));
            InsertProxy = new InsertProxy<TModel>();
        }

        public MultipleInsertByProxy(IEnumerable<TModel> readyModels, IInsertProxy<TModel> insertProxy)
        {
            ReadyModels = readyModels ?? throw new ArgumentNullException(nameof(readyModels));
            InsertProxy = insertProxy ?? throw new ArgumentNullException(nameof(insertProxy));
        }

        public IInsertProxy<TModel> InsertProxy { get; }

        IInsertProxy IMultipleInsertByProxyOnReadyModels.InsertProxy => InsertProxy;

        public IEnumerable<TModel> ReadyModels { get; }

        IEnumerable<object> IReadyModels.ReadyModels => throw new NotImplementedException();

        protected override async IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(InsertProxy.ModelQualifierGroup.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = InsertProxy.InsertedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                foreach (TModel readyModel in ReadyModels)
                {
                    TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(readyModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    if (!(insertedModel is null))
                    {
                        yield return insertedModel;
                    }
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(InsertProxy.ModelQualifierGroup.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = InsertProxy.InsertedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                ICollection<TModel> insertedModels = new Collection<TModel>();

                //for (int i = 0; i < insertCount; i++)
                //{
                //    TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                //    if (insertedModel is null)
                //    {
                //        continue;
                //    }

                //    insertedModels.Add(insertedModel);
                //}

                if (insertedModels.Count > 0)
                {
                    return insertedModels;
                }
            }

            return null;
        }
    }
}