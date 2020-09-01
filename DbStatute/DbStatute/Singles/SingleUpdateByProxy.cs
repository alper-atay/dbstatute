using DbStatute.Builders;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdateByProxy<TModel, TUpdateProxy> : SingleUpdateBase<TModel>, ISingleUpdateByProxy<TUpdateProxy>
        where TModel : class, IModel, new()
        where TUpdateProxy : IUpdateProxy<TModel>
    {
        public TUpdateProxy UpdateProxy { get; }

        protected override async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            IModelBuilder<TModel> modelBuilder = UpdateProxy.ModelBuilder;
            IFieldQualifier<TModel> fieldQualifier = UpdateProxy.FieldQualifier;

            bool isModelBuilt = modelBuilder.Build(out TModel model);
            Logs.AddRange(modelBuilder.ReadOnlyLogs);

            if (isModelBuilt)
            {
                FieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(fieldQualifier);
                fieldBuilder.Build(out IEnumerable<Field> fields);
                Logs.AddRange(fieldBuilder.ReadOnlyLogs);

                int updateCount = await dbConnection.UpdateAsync(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (updateCount > 0)
                {
                    TModel updateModel = await dbConnection.QueryAsync<TModel>(model.Id, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
                        .ContinueWith(x => x.Result.FirstOrDefault());

                    return updateModel;
                }
            }

            return null;
        }
    }
}