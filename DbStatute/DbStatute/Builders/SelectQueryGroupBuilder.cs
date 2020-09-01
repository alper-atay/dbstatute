using DbStatute.Extensions;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;

namespace DbStatute.Builders
{
    public class SelectQueryGroupBuilder<TModel> : Builder<QueryGroup>, ISelectQueryGroupBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public Conjunction Conjunction { get; set; } = Conjunction.And;

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier IQueryGroupBuilder.FieldQualifier => FieldQualifier;

        public IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }

        IOperationFieldQualifier ISelectQueryGroupBuilder.OperationFieldQualifier => OperationFieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier IQueryGroupBuilder.PredicateFieldQualifier => PredicateFieldQualifier;

        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }

        IValueFieldQualifier IQueryGroupBuilder.ValueFieldQualifier => ValueFieldQualifier;

        protected override bool BuildOperation(out QueryGroup built)
        {
            built = null;

            Logs.AddRange(QueryFieldExtension.CreateQueryFields<TModel>(FieldQualifier, ValueFieldQualifier, PredicateFieldQualifier, OperationFieldQualifier, out IEnumerable<QueryField> queryFields));

            if (ReadOnlyLogs.Safely)
            {
                built = new QueryGroup(queryFields, Conjunction);
            }

            return !(built is null);
        }
    }
}