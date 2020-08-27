using DbStatute.Interfaces.Querying.Fundamentals;
using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute.Interfaces.Querying
{
    public interface IUpdateQuery : IStatuteQueryBase
    {
        IModelQueryQualifier ModelQueryQualifier { get; }
    }

    public interface IUpdateQuery<TModel> : IStatuteQueryBase<TModel>, IUpdateQuery
        where TModel : class, IModel, new()
    {
        new IModelQueryQualifier<TModel> ModelQueryQualifier { get; }
    }
}