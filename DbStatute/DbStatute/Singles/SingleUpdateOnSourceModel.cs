using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdateOnSourceModel<TModel> : SingleUpdateBase<TModel>, ISingleUpdateOnSourceModel<TModel>
        where TModel : class, IModel, new()
    {
        public SingleUpdateOnSourceModel(TModel sourceModel)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public SingleUpdateOnSourceModel(TModel sourceModel, IFieldQualifier<TModel> fieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier ISingleUpdateOnSourceModel.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier ISingleUpdateOnSourceModel.PredicateFieldQualifier => PredicateFieldQualifier;

        public TModel SourceModel { get; }

        object ISourceModel.SourceModel => SourceModel;

        protected override async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(SourceModel.Predicate(FieldQualifier, PredicateFieldQualifier, out IEnumerable<Field> fields));

            if (ReadOnlyLogs.Safely)
            {
                int updatedCount = await dbConnection.UpdateAsync(SourceModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (updatedCount > 0)
                {
                    return SourceModel;
                }
            }

            return null;
        }
    }
}