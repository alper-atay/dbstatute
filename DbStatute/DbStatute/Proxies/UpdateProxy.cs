using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using DbStatute.Queries;
using System;

namespace DbStatute.Proxies
{
    public class UpdateProxy : UpdateProxyBase, IUpdateProxy
    {
        public UpdateProxy(object sourceModel)
        {
            SourceModel = sourceModel;
            WhereQuery = new WhereQuery();
            UpdatedFieldQualifier = new FieldQualifier();
            PredicateFieldQualifier = new PredicateFieldQualifier();
        }

        public UpdateProxy(object sourceModel, IWhereQuery whereQuery, IFieldQualifier updatedFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier)
        {
            SourceModel = sourceModel;
            WhereQuery = whereQuery ?? throw new ArgumentNullException(nameof(whereQuery));
            UpdatedFieldQualifier = updatedFieldQualifier ?? throw new ArgumentNullException(nameof(updatedFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IPredicateFieldQualifier PredicateFieldQualifier { get; }

        public object SourceModel { get; }

        public IFieldQualifier UpdatedFieldQualifier { get; }

        public IWhereQuery WhereQuery { get; }
    }

    public class UpdateProxy<TModel> : UpdateProxyBase<TModel>, IUpdateProxy<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateProxy()
        {
            WhereQuery = new WhereQuery<TModel>();
            UpdatedFieldQualifier = new FieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public UpdateProxy(IWhereQuery<TModel> whereQuery, IPredicateFieldQualifier<TModel> predicateFieldQualifier, IFieldQualifier<TModel> updatedFieldQualifier)
        {
            WhereQuery = whereQuery ?? throw new ArgumentNullException(nameof(whereQuery));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
            UpdatedFieldQualifier = updatedFieldQualifier ?? throw new ArgumentNullException(nameof(updatedFieldQualifier));
        }

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier IUpdateProxy.PredicateFieldQualifier => PredicateFieldQualifier;

        public TModel SourceModel { get; }

        object ISourceModel.SourceModel => SourceModel;

        public IFieldQualifier<TModel> UpdatedFieldQualifier { get; }

        IFieldQualifier IUpdateProxy.UpdatedFieldQualifier => UpdatedFieldQualifier;

        public IWhereQuery<TModel> WhereQuery { get; }

        IWhereQuery IUpdateProxy.WhereQuery => WhereQuery;
    }
}