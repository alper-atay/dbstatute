using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Interfaces.Singles;
using DbStatute.Querying.Builders;
using DbStatute.Querying.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByRawModel<TModel, TFieldQualifier> : SingleInsertBase<TModel>, ISingleInsertByRawModel<TModel, TFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : class, IFieldQualifier<TModel>
    {
        public SingleInsertByRawModel(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = new FieldQualifier<TModel>() as TFieldQualifier;
        }

        public SingleInsertByRawModel(TModel rawModel, TFieldQualifier fieldQualifier)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public TFieldQualifier FieldQualifier { get; }
        public TModel RawModel { get; }
        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            IFieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(FieldQualifier);
            fieldBuilder.Build(out IEnumerable<Field> fields);
            Logs.AddRange(fieldBuilder.ReadOnlyLogs);

            TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(RawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            return insertedModel;
        }
    }
}