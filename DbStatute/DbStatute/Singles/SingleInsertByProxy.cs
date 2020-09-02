using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers.Groups;
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
            IModelQualifierGroup<TModel> modelBuilder = InsertProxy.ModelQualifierGroup;
            Logs.AddRange(modelBuilder.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = InsertProxy.InsertedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                return await dbConnection.InsertAsync<TModel, TModel>(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}