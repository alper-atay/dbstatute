using DbStatute.Extensions;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Qualifiers;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Builders
{
    public class QueryGroupBuilder<TModel> : Builder<QueryGroup>, IQueryGroupBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public QueryGroupBuilder()
        {
            FieldQualifier = new FieldQualifier<TModel>();
            ValueFieldQualifier = new ValueFieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public QueryGroupBuilder(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            FieldQualifier = fieldQualifier;
            ValueFieldQualifier = valueFieldQualifier;
            PredicateFieldQualifier = predicateFieldQualifier;
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IQueryGroupBuilder.FieldQualifier => FieldQualifier;
        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        IPredicateFieldQualifier IQueryGroupBuilder.PredicateFieldQualifier => PredicateFieldQualifier;
        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
        IValueFieldQualifier IQueryGroupBuilder.ValueFieldQualifier => ValueFieldQualifier;

        protected override bool BuildOperation(out QueryGroup built)
        {
            built = null;

            Logs.AddRange(QueryFieldExtension.CreateQueryFields<TModel>(FieldQualifier, ValueFieldQualifier, PredicateFieldQualifier, out IEnumerable<QueryField> queryFields));

            if (ReadOnlyLogs.Safely)
            {
                built = new QueryGroup(queryFields);
            }

            return !(built is null);
        }
    }
}