using Basiclog;
using DbStatute.Enumerations;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Querying.Qualifiers;
using DbStatute.Querying.Qualifiers.Fields;
using RepoDb;

namespace DbStatute.Querying.Builders
{
    public abstract class QueryGroupBuilder : IQueryGroupBuilder
    {

        protected QueryGroupBuilder(QueryGroupUsage usage)
        {
            Usage = usage;
            FieldBuilder = new FieldBuilder();
            ValueFieldQualifier = new ValueFieldQualifier();
            PredicateFieldQualifier = new PredicateFieldQualifier();
        }

        protected QueryGroupBuilder(QueryGroupUsage usage, IFieldBuilder fieldBuilder)
        {
            Usage = usage;
            FieldBuilder = fieldBuilder;
            ValueFieldQualifier = new ValueFieldQualifier();
            PredicateFieldQualifier = new PredicateFieldQualifier();
        }

        protected QueryGroupBuilder(QueryGroupUsage usage, IFieldBuilder fieldBuilder, IValueFieldQualifier valueFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier)
        {
            Usage = usage;
            FieldBuilder = fieldBuilder;
            ValueFieldQualifier = valueFieldQualifier;
            PredicateFieldQualifier = predicateFieldQualifier;
        }

        public IFieldBuilder FieldBuilder { get; }
        public IPredicateFieldQualifier PredicateFieldQualifier { get; }
        public QueryGroupUsage Usage { get; }
        public IValueFieldQualifier ValueFieldQualifier { get; }

        public abstract IReadOnlyLogbook Build(out QueryGroup queryGroup);
    }

    public abstract class QueryGroupBuilder<TModel> : IQueryGroupBuilder<TModel>
        where TModel : class, IModel, new()
    {
        protected QueryGroupBuilder(QueryGroupUsage usage)
        {
            Usage = usage;
            FieldBuilder = new FieldBuilder<TModel>();
            ValueFieldQualifier = new ValueFieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        protected QueryGroupBuilder(QueryGroupUsage usage, IFieldBuilder<TModel> fieldBuilder)
        {
            Usage = usage;
            FieldBuilder = fieldBuilder;
            ValueFieldQualifier = new ValueFieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        protected QueryGroupBuilder(QueryGroupUsage usage, IFieldBuilder<TModel> fieldBuilder, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            Usage = usage;
            FieldBuilder = fieldBuilder;
            ValueFieldQualifier = valueFieldQualifier;
            PredicateFieldQualifier = predicateFieldQualifier;
        }

        public IFieldBuilder<TModel> FieldBuilder { get; }
        IFieldBuilder IQueryGroupBuilder.FieldBuilder => FieldBuilder;
        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        IPredicateFieldQualifier IQueryGroupBuilder.PredicateFieldQualifier => PredicateFieldQualifier;
        public QueryGroupUsage Usage { get; }
        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
        IValueFieldQualifier IQueryGroupBuilder.ValueFieldQualifier => ValueFieldQualifier;

        public abstract IReadOnlyLogbook Build(out QueryGroup queryGroup);
    }
}