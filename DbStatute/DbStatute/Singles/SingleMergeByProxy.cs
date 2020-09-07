using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Singles;
using DbStatute.Proxies;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleMergeByProxy<TModel> : SingleMergeBase<TModel>, ISingleMergeByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleMergeByProxy()
        {
            MergeProxy = new MergeProxy<TModel>();
        }

        public SingleMergeByProxy(IMergeProxy<TModel> mergeProxy)
        {
            MergeProxy = mergeProxy ?? throw new ArgumentNullException(nameof(mergeProxy));
        }

        public IMergeProxy<TModel> MergeProxy { get; }

        IMergeProxy ISingleMergeByProxy.MergeProxy => MergeProxy;

        protected override async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(MergeProxy.ModelQuery.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                IFieldQualifier<TModel> mergedFieldQualifier = MergeProxy.MergedFieldQualifier;
                bool mergedFieldsBuilt = mergedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!mergedFieldsBuilt)
                {
                    fields = null;
                }

                return await dbConnection.MergeAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}