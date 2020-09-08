using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Queries
{
    public class SearchQuery : ISearchQuery
    {
        public SearchQuery()
        {
            Fields = new FieldQualifier();
            ValueMap = new ValueFieldQualifier();
            PredicateMap = new PredicateFieldQualifier();
            OperationMap = new OperationFieldQualifier();
        }

        public SearchQuery(IFieldQualifier fields, IValueFieldQualifier valueMap, IPredicateFieldQualifier predicateMap, IOperationFieldQualifier operationMap)
        {
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
            ValueMap = valueMap ?? throw new ArgumentNullException(nameof(valueMap));
            PredicateMap = predicateMap ?? throw new ArgumentNullException(nameof(predicateMap));
            OperationMap = operationMap ?? throw new ArgumentNullException(nameof(operationMap));
        }

        public IFieldQualifier Fields { get; }

        public IOperationFieldQualifier OperationMap { get; }

        public IPredicateFieldQualifier PredicateMap { get; }

        public IValueFieldQualifier ValueMap { get; }
    }

    public class SearchQuery<TModel> : ISearchQuery<TModel>
        where TModel : class, IModel, new()
    {
        public SearchQuery()
        {
            Fields = new FieldQualifier<TModel>();
            ValueMap = new ValueFieldQualifier<TModel>();
            PredicateMap = new PredicateFieldQualifier<TModel>();
            OperationMap = new OperationFieldQualifier<TModel>();
        }

        public SearchQuery(IFieldQualifier<TModel> fields, IValueFieldQualifier<TModel> valueMap, IPredicateFieldQualifier<TModel> predicateMap, IOperationFieldQualifier<TModel> operationMap)
        {
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
            ValueMap = valueMap ?? throw new ArgumentNullException(nameof(valueMap));
            PredicateMap = predicateMap ?? throw new ArgumentNullException(nameof(predicateMap));
            OperationMap = operationMap ?? throw new ArgumentNullException(nameof(operationMap));
        }

        public IFieldQualifier<TModel> Fields { get; }

        IFieldQualifier ISearchQuery.Fields => Fields;

        public IOperationFieldQualifier<TModel> OperationMap { get; }

        IOperationFieldQualifier ISearchQuery.OperationMap => OperationMap;

        public IPredicateFieldQualifier<TModel> PredicateMap { get; }

        IPredicateFieldQualifier ISearchQuery.PredicateMap => PredicateMap;

        public IValueFieldQualifier<TModel> ValueMap { get; }

        IValueFieldQualifier ISearchQuery.ValueMap => ValueMap;
    }
}