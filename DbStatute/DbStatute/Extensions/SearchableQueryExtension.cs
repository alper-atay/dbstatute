using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using RepoDb;

namespace DbStatute.Extensions
{
    public static class SearchableQueryExtension
    {
        public static IReadOnlyLogbook Build<TModel>(this ISearchableQuery<TModel> searchableQuery, out QueryGroup queryGroup) where TModel : class, IModel, new()
        {
            return searchableQuery.SearchQuery.Build(searchableQuery.Conjunction, out queryGroup);
        }
    }
}