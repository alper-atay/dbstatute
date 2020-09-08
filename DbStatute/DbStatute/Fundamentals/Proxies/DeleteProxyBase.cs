using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Queries;
using DbStatute.Queries;
using RepoDb.Enumerations;
using System;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class DeleteProxyBase : ProxyBase, IDeleteProxyBase
    {
        protected DeleteProxyBase()
        {
            SearchQuery = new SearchQuery();
        }

        protected DeleteProxyBase(ISearchQuery searchQuery)
        {
            SearchQuery = searchQuery ?? throw new ArgumentNullException(nameof(searchQuery));
        }

        public Conjunction Conjunction { get; set; }

        public ISearchQuery SearchQuery { get; }
    }

    public abstract class DeleteProxyBase<TModel> : ProxyBase<TModel>, IDeleteProxyBase<TModel>
        where TModel : class, IModel, new()
    {
        protected DeleteProxyBase()
        {
            SearchQuery = new SearchQuery<TModel>();
        }

        protected DeleteProxyBase(ISearchQuery<TModel> searchQuery)
        {
            SearchQuery = searchQuery ?? throw new ArgumentNullException(nameof(searchQuery));
        }

        public Conjunction Conjunction { get; set; }

        public ISearchQuery<TModel> SearchQuery { get; }

        ISearchQuery ISearchableQuery.SearchQuery => SearchQuery;
    }
}