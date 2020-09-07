using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Proxies
{
    public class UpdateProxyOnRawModel : UpdateProxyBase, IUpdateProxyOnRawModel
    {
        public UpdateProxyOnRawModel()
        {
            FieldQualifier = new FieldQualifier();
            PredicateFieldQualifier = new PredicateFieldQualifier();
        }

        public IFieldQualifier FieldQualifier { get; }

        public IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public class UpdateProxyOnRawModel<TModel> : UpdateProxyBase<TModel>, IUpdateProxyOnRawModel<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateProxyOnRawModel()
        {
            FieldQualifier = new FieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public UpdateProxyOnRawModel(IFieldQualifier<TModel> fieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier IUpdateProxyOnRawModel.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier IUpdateProxyOnRawModel.PredicateFieldQualifier => PredicateFieldQualifier;
    }
}