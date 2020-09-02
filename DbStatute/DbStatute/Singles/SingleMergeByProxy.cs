﻿using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
using DbStatute.Interfaces.Singles;
using DbStatute.Querying;
using RepoDb;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleMergeByProxy<TModel, TMergeProxy> : SingleMergeBase<TModel>, ISingleMergeByProxy<TModel, TMergeProxy>
        where TModel : class, IModel, new()
        where TMergeProxy : class, IMergeProxy<TModel>
    {
        public SingleMergeByProxy()
        {
            MergeProxy = new MergeProxy<TModel>() as TMergeProxy;
        }

        public SingleMergeByProxy(TMergeProxy mergeProxy)
        {
            MergeProxy = mergeProxy ?? throw new System.ArgumentNullException(nameof(mergeProxy));
        }

        public TMergeProxy MergeProxy { get; }

        protected override async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            IModelQualifierGroup<TModel> modelQualifierGroup = MergeProxy.ModelQualifierGroup;

            Logs.AddRange(modelQualifierGroup.Build<TModel>(out TModel model));

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