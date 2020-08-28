using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb.Enumerations;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface ISelectQueryGroupBuilder : IQueryGroupBuilder
    {
        Conjunction Conjunction { get; set; }

        #region Qualifiers

        IOperationFieldQualifier OperationFieldQualifier { get; }

        #endregion Qualifiers
    }

    public interface ISelectQueryGroupBuilder<TModel> : ISelectQueryGroupBuilder
        where TModel : class, IModel, new()
    {
        #region Qualifiers

        new IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }

        #endregion Qualifiers
    }
}