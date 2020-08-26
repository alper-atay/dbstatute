using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute.Interfaces.Querying
{
    public interface IMergeQuery : IStatuteQuery
    {
        IQueryQualifier QueryQualifier { get; }
    }

    public interface IMergerQuery<TModel> : IStatuteQuery<TModel>, IMergeQuery
        where TModel : class, IModel, new()
    {
        new IQueryQualifier<TModel> QueryQualifier { get; }
    }
}