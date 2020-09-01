using DbStatute.Interfaces.Qualifiers;
using RepoDb.Enumerations;

namespace DbStatute.Interfaces.Builders
{
    public interface ISelectQueryGroupBuilder : IQueryGroupBuilder
    {
        Conjunction Conjunction { get; set; }
        IOperationFieldQualifier OperationFieldQualifier { get; }
    }

    public interface ISelectQueryGroupBuilder<TModel> : IQueryGroupBuilder<TModel>, ISelectQueryGroupBuilder
        where TModel : class, IModel, new()
    {
        new IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }
    }
}