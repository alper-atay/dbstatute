using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface ISearchableQuery
    {
        ISearchQuery SearchQuery { get; }
    }

    public interface ISearchableQuery<TModel> : ISearchableQuery
        where TModel : class, IModel, new()
    {
        new ISearchQuery<TModel> SearchQuery { get; }
    }
}