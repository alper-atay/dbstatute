using DbStatute.Interfaces.Fundamentals.Enumerables;
using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;
using RepoDb.Enumerations;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IOperationFieldQualifier : ISettableSpecializedField<Operation>, IFieldOperationPairs
    {
    }

    public interface IOperationFieldQualifier<TModel> : ISettableSpecializedField<TModel, Operation>, IOperationFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}