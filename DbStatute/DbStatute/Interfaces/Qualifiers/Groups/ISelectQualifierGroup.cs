using RepoDb.Enumerations;

namespace DbStatute.Interfaces.Qualifiers.Groups
{
    public interface ISelectQualifierGroup : IModelQualifierGroup
    {
        Conjunction Conjunction { get; set; }

        IOperationFieldQualifier OperationFieldQualifier { get; }
    }

    public interface ISelectQualifierGroup<TModel> : IModelQualifierGroup<TModel>, ISelectQualifierGroup
        where TModel : class, IModel, new()
    {
        new IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }
    }
}