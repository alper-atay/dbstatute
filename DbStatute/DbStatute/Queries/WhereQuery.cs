using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Queries
{
    public class WhereQuery : IWhereQuery
    {
        public WhereQuery()
        {
            FieldQualifier = new FieldQualifier();
            ValueFieldQualifier = new ValueFieldQualifier();
            PredicateFieldQualifier = new PredicateFieldQualifier();
            OperationFieldQualifier = new OperationFieldQualifier();
        }

        public WhereQuery(IFieldQualifier fieldQualifier, IValueFieldQualifier valueFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier, IOperationFieldQualifier operationFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
            OperationFieldQualifier = operationFieldQualifier ?? throw new ArgumentNullException(nameof(operationFieldQualifier));
        }

        public IFieldQualifier FieldQualifier { get; }

        public IOperationFieldQualifier OperationFieldQualifier { get; }

        public IPredicateFieldQualifier PredicateFieldQualifier { get; }

        public IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public class WhereQuery<TModel> : IWhereQuery<TModel>
        where TModel : class, IModel, new()
    {
        public WhereQuery()
        {
            FieldQualifier = new FieldQualifier<TModel>();
            ValueFieldQualifier = new ValueFieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
            OperationFieldQualifier = new OperationFieldQualifier<TModel>();
        }

        public WhereQuery(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier, IOperationFieldQualifier<TModel> operationFieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            ValueFieldQualifier = valueFieldQualifier ?? throw new ArgumentNullException(nameof(valueFieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
            OperationFieldQualifier = operationFieldQualifier ?? throw new ArgumentNullException(nameof(operationFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier IWhereQuery.FieldQualifier => FieldQualifier;

        public IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }

        IOperationFieldQualifier IWhereQuery.OperationFieldQualifier => OperationFieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier IWhereQuery.PredicateFieldQualifier => PredicateFieldQualifier;

        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }

        IValueFieldQualifier IWhereQuery.ValueFieldQualifier => ValueFieldQualifier;
    }
}