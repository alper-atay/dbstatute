using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Singles;
using DbStatute.Proxies;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdateByProxy<TModel> : SingleUpdateBase<TModel>, ISingleUpdateByProxy
        where TModel : class, IModel, new()
    {
        public SingleUpdateByProxy()
        {
            UpdateProxy = new UpdateProxy<TModel>();
        }

        public SingleUpdateByProxy(IUpdateProxy<TModel> updateProxy)
        {
            UpdateProxy = updateProxy ?? throw new System.ArgumentNullException(nameof(updateProxy));
        }

        public IUpdateProxy<TModel> UpdateProxy { get; }

        IUpdateProxy ISingleUpdateByProxy.UpdateProxy => UpdateProxy;

        protected override async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            //IModelQualifierGroup<TModel> modelQualifierGroup = UpdateProxy.ModelQualifierGroup;
            //Logs.AddRange(modelQualifierGroup.Build(out TModel model));

            //if (ReadOnlyLogs.Safely)
            //{
            //    IFieldQualifier<TModel> mergedFieldQualifier = UpdateProxy.FieldQualifier;
            //    bool mergedFieldBuilt = mergedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

            //    if (!mergedFieldBuilt)
            //    {
            //        fields = null;
            //    }

            //    int updateCount = await dbConnection.UpdateAsync(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            //    if (updateCount > 0)
            //    {
            //        TModel updateModel = await dbConnection.QueryAsync<TModel>(model.Id, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
            //            .ContinueWith(x => x.Result.FirstOrDefault());

            //        return updateModel;
            //    }
            //}

            return null;
        }
    }
}