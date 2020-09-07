using DbStatute.Fundamentals.Singles;
using DbStatute.Helpers;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleMergeByRawModel<TModel> : SingleMergeBase<TModel>, ISingleMergeByRawModel<TModel>
        where TModel : class, IModel, new()
    {
        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier ISingleMergeByRawModel.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier ISingleMergeByRawModel.PredicateFieldQualifier => PredicateFieldQualifier;

        public TModel RawModel { get; }

        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(RawModelHelper.PredicateModel(RawModel, FieldQualifier, PredicateFieldQualifier, out IEnumerable<Field> fields));

            if (ReadOnlyLogs.Safely)
            {
                return await dbConnection.MergeAsync<TModel, TModel>(RawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}