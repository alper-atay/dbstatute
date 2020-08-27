using DbStatute.Interfaces.Querying.Fundamentals;
using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute.Interfaces.Querying
{
    public interface IInsertQuery : IStatuteQueryBase
    {
        IModelQueryQualifier ModelQueryQualifier { get; }
    }

    public interface IInsertQuery<TModel> : IStatuteQueryBase<TModel>, IInsertQuery
        where TModel : class, IModel, new()
    {
        new IModelQueryQualifier<TModel> ModelQueryQualifier { get; }
    }
}