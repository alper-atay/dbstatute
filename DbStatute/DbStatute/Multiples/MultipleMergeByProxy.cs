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
    public class MultipleMergeByProxy<TModel> : MultipleMergeBase<TModel>, IMultipleMergeByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleMergeByProxy()
        {
            MergeProxy = new MergeProxy<TModel>();
        }

        public MultipleMergeByProxy(IMergeProxy<TModel> mergeProxy)
        {
            MergeProxy = mergeProxy ?? throw new ArgumentNullException(nameof(mergeProxy));
        }

        public IMergeProxy<TModel> MergeProxy { get; }

        IMergeProxy IMultipleMergeByProxy.MergeProxy => MergeProxy;

        protected override async IAsyncEnumerable<TModel> MergeAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(MergeProxy.ModelQualifierGroup.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = MergeProxy.MergedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                //for (int i = 0; i < mergeCount; i++)
                //{
                //    TModel insertedModel = await dbConnection.MergeAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                //    if (!(insertedModel is null))
                //    {
                //        yield return insertedModel;
                //    }
                //}

                yield return null;
            }
        }

        protected override async Task<IEnumerable<TModel>> MergeOperationAsync(IDbConnection dbConnection)
        {

            Logs.AddRange(MergeProxy.ModelQualifierGroup.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = MergeProxy.MergedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                ICollection<TModel> mergedModels = new Collection<TModel>();

                //for (int i = 0; i < mergeCount; i++)
                //{
                //    TModel mergedModel = await dbConnection.MergeAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                //    if (!(mergedModel is null))
                //    {
                //        mergedModels.Add(mergedModel);
                //    }
                //}

                if (mergedModels.Count > 0)
                {
                    return mergedModels;
                }
            }

            return null;
        }
    }
}