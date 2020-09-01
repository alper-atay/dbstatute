using DbStatute.Builders;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByProxy<TModel, TInsertProxy> : SingleInsertBase<TModel>, ISingleInsertByProxy<TModel, TInsertProxy>
        where TModel : class, IModel, new()
        where TInsertProxy : IInsertProxy<TModel>
    {
        public SingleInsertByProxy(TInsertProxy insertProxy)
        {
            InsertProxy = insertProxy ?? throw new ArgumentNullException(nameof(insertProxy));
        }

        public TInsertProxy InsertProxy { get; }

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            IModelBuilder<TModel> modelBuilder = InsertProxy.ModelBuilder;
            modelBuilder.Build(out TModel model);
            Logs.AddRange(modelBuilder.ReadOnlyLogs);

            IFieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(InsertProxy.InsertedFieldQualifier);
            fieldBuilder.Build(out IEnumerable<Field> fields);
            Logs.AddRange(fieldBuilder.ReadOnlyLogs);

            if (model != null)
            {
                return await dbConnection.InsertAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}