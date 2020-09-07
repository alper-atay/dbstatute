using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Queries
{
    public class ModelQuery : IModelQuery
    {
        public ModelQuery()
        {
            FieldQualifier = new FieldQualifier();
            ValueFieldQualifier = new ValueFieldQualifier();
            PredicateFieldQualifier = new PredicateFieldQualifier();
        }

        public ModelQuery(IFieldQualifier fieldQualifier, IValueFieldQualifier valueFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier FieldQualifier { get; }

        public IPredicateFieldQualifier PredicateFieldQualifier { get; }

        public IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public class ModelQuery<TModel> : IModelQuery<TModel>
        where TModel : class, IModel, new()
    {
        public ModelQuery()
        {
            FieldQualifier = new FieldQualifier<TModel>();
            ValueFieldQualifier = new ValueFieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public ModelQuery(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier IModelQuery.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier IModelQuery.PredicateFieldQualifier => PredicateFieldQualifier;

        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }

        IValueFieldQualifier IModelQuery.ValueFieldQualifier => ValueFieldQualifier;
    }
}