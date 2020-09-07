using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Helpers;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Queries;
using DbStatute.Proxies;
using DbStatute.Queries;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleUpdateByProxyWithSelectQuery<TModel> : MultipleUpdateBase<TModel>, IMultipleUpdateByProxyWithWhereQuery<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleUpdateByProxyWithSelectQuery(IEnumerable<TModel> rawModels)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            UpdateProxy = new UpdateProxy<TModel>();
            WhereQuery = new WhereQuery<TModel>();
        }

        public MultipleUpdateByProxyWithSelectQuery(IEnumerable<TModel> rawModels, IUpdateProxy<TModel> updateProxy, IWhereQuery<TModel> whereQuery)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            UpdateProxy = updateProxy ?? throw new ArgumentNullException(nameof(updateProxy));
            WhereQuery = whereQuery ?? throw new ArgumentNullException(nameof(whereQuery));
        }

        public IEnumerable<TModel> RawModels { get; }

        IEnumerable<object> IRawModels.RawModels => RawModels;

        public IUpdateProxy<TModel> UpdateProxy { get; }

        IUpdateProxy IMultipleUpdateByProxy.UpdateProxy => UpdateProxy;

        public IWhereQuery<TModel> WhereQuery { get; }

        IWhereQuery IMultipleUpdateByProxyWithWhereQuery.WhereQuery => WhereQuery;

        protected override IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(WhereQuery.Build(Conjunction.And, out QueryGroup queryGroup));

            if(!ReadOnlyLogs.Safely)
            {
                yield break;
            }

            

            foreach (TModel rawModel in RawModels)
            {
                dbConnection.UpdateAsync<TModel>(rawModel, queryGroup,   );
            }


        }

        protected override Task<IEnumerable<TModel>> UpdateOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}