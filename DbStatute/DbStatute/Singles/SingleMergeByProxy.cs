using DbStatute.Builders;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
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
            IModelBuilder<TModel> modelBuilder = MergeProxy.ModelBuilder;
            IFieldQualifier<TModel> mergedFieldQualifier = MergeProxy.MergedFieldQualifier;

            modelBuilder.Build(out TModel model);
            Logs.AddRange(modelBuilder.ReadOnlyLogs);

            IFieldBuilder<TModel> mergedFieldBuilder = new FieldBuilder<TModel>(mergedFieldQualifier);
            mergedFieldBuilder.Build(out IEnumerable<Field> fields);
            Logs.AddRange(mergedFieldBuilder.ReadOnlyLogs);

            if (ReadOnlyLogs.Safely)
            {
                return await dbConnection.MergeAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}