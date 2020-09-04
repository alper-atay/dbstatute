using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Proxies
{
    public class UpdateProxy : ProxyBase, IUpdateProxy
    {
        public UpdateProxy()
        {
            FieldQualifier = new FieldQualifier();
            ValueFieldQualifier = new ValueFieldQualifier();
            PredicateFieldQualifier = new PredicateFieldQualifier();
        }

        public UpdateProxy(IFieldQualifier fieldQualifier, IValueFieldQualifier valueFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier FieldQualifier { get; }

        public IPredicateFieldQualifier PredicateFieldQualifier { get; }

        public IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public class UpdateProxy<TModel> : ProxyBase<TModel>, IUpdateProxy<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateProxy()
        {
            FieldQualifier = new FieldQualifier<TModel>();
            ValueFieldQualifier = new ValueFieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public UpdateProxy(IFieldQualifier<TModel> updatedFieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            FieldQualifier = updatedFieldQualifier ?? throw new ArgumentNullException(nameof(updatedFieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier IUpdateProxy.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier IUpdateProxy.PredicateFieldQualifier => PredicateFieldQualifier;

        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }

        IValueFieldQualifier IUpdateProxy.ValueFieldQualifier => ValueFieldQualifier;
    }
}