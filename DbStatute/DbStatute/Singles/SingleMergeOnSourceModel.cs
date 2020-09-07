using DbStatute.Extensions;
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
    public class SingleMergeOnSourceModel<TModel> : SingleMergeBase<TModel>, ISingleMergeOnSourceModel<TModel>
        where TModel : class, IModel, new()
    {
        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier ISingleMergeOnSourceModel.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier ISingleMergeOnSourceModel.PredicateFieldQualifier => PredicateFieldQualifier;

        public TModel SourceModel { get; }

        object ISourceModel.SourceModel => SourceModel;

        protected override async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(SourceModel.Predicate(FieldQualifier, PredicateFieldQualifier, out IEnumerable<Field> fields));

            if (ReadOnlyLogs.Safely)
            {
                return await dbConnection.MergeAsync<TModel, TModel>(SourceModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}