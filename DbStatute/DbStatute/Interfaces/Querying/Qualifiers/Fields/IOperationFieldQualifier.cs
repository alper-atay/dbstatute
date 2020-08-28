using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;
using RepoDb.Enumerations;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IOperationFieldQualifier : ISettableSpecializedField<Operation>, IFieldOperationMap
    {
        IFieldQualifier FieldQualifier { get; }
    }

    public interface IOperationFieldQualifier<TModel> : ISettableSpecializedField<TModel, Operation>, IOperationFieldQualifier
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
    }
}