using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;
using RepoDb.Enumerations;

namespace DbStatute.Interfaces.Querying.Qualifiers.Fields
{
    public interface IOperationFieldQualifier : ISettableSpecializedField<Operation>, IFieldOperationMap
    {
    }

    public interface IOperationFieldQualifier<TModel> : ISettableSpecializedField<TModel, Operation>, IOperationFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}