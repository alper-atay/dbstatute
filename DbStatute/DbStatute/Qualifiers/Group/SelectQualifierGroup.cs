using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
using RepoDb.Enumerations;
using System;

namespace DbStatute.Qualifiers.Group
{
    public class SelectQualifierGroup : ModelQualifierGroup, ISelectQualifierGroup
    {
        public SelectQualifierGroup() : base()
        {
            OperationFieldQualifier = new OperationFieldQualifier();
        }

        public SelectQualifierGroup(IFieldQualifier fieldQualifier, IValueFieldQualifier valueFieldQualifier, IOperationFieldQualifier operationFieldQualifier) : base(fieldQualifier, valueFieldQualifier)
        {
            OperationFieldQualifier = operationFieldQualifier ?? throw new ArgumentNullException(nameof(operationFieldQualifier));
        }

        public SelectQualifierGroup(IFieldQualifier fieldQualifier, IValueFieldQualifier valueFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier, IOperationFieldQualifier operationFieldQualifier) : base(fieldQualifier, valueFieldQualifier, predicateFieldQualifier)
        {
            OperationFieldQualifier = operationFieldQualifier ?? throw new ArgumentNullException(nameof(operationFieldQualifier));
        }

        public Conjunction Conjunction { get; set; }

        public IOperationFieldQualifier OperationFieldQualifier { get; }
    }

    public class SelectQualifierGroup<TModel> : ModelQualifierGroup<TModel>, ISelectQualifierGroup<TModel>
        where TModel : class, IModel, new()
    {
        public SelectQualifierGroup() : base()
        {
            OperationFieldQualifier = new OperationFieldQualifier<TModel>();
        }

        public SelectQualifierGroup(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IOperationFieldQualifier<TModel> operationFieldQualifier) : base(fieldQualifier, valueFieldQualifier)
        {
            OperationFieldQualifier = operationFieldQualifier ?? throw new ArgumentNullException(nameof(operationFieldQualifier));
        }

        public SelectQualifierGroup(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier, IOperationFieldQualifier<TModel> operationFieldQualifier) : base(fieldQualifier, valueFieldQualifier, predicateFieldQualifier)
        {
            OperationFieldQualifier = operationFieldQualifier ?? throw new ArgumentNullException(nameof(operationFieldQualifier));
        }

        public Conjunction Conjunction { get; set; }

        public IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }

        IOperationFieldQualifier ISelectQualifierGroup.OperationFieldQualifier => OperationFieldQualifier;
    }
}