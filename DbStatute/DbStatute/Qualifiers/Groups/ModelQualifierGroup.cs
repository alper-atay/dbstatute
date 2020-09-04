using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
using System;

namespace DbStatute.Qualifiers.Groups
{
    public class ModelQualifierGroup : IModelQualifierGroup
    {
        public ModelQualifierGroup()
        {
            FieldQualifier = new FieldQualifier();
            ValueFieldQualifier = new ValueFieldQualifier();
            PredicateFieldQualifier = new PredicateFieldQualifier();
        }

        public ModelQualifierGroup(IFieldQualifier fieldQualifier, IValueFieldQualifier valueFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = new PredicateFieldQualifier();
        }

        public ModelQualifierGroup(IFieldQualifier fieldQualifier, IValueFieldQualifier valueFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier FieldQualifier { get; }

        public IPredicateFieldQualifier PredicateFieldQualifier { get; }

        public IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public class ModelQualifierGroup<TModel> : IModelQualifierGroup<TModel>
        where TModel : class, IModel, new()
    {
        public ModelQualifierGroup()
        {
            FieldQualifier = new FieldQualifier<TModel>();
            ValueFieldQualifier = new ValueFieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public ModelQualifierGroup(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public ModelQualifierGroup(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier IModelQualifierGroup.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier IModelQualifierGroup.PredicateFieldQualifier => PredicateFieldQualifier;

        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }

        IValueFieldQualifier IModelQualifierGroup.ValueFieldQualifier => ValueFieldQualifier;
    }
}