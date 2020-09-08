using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Queries;
using DbStatute.Queries;
using System;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class SelectProxyBase : ProxyBase, ISelectProxyBase
    {
        protected SelectProxyBase()
        {
            SearchQuery = new SearchQuery();
        }

        protected SelectProxyBase(ISearchQuery searchQuery)
        {
            SearchQuery = searchQuery ?? throw new ArgumentNullException(nameof(searchQuery));
        }

        public ISearchQuery SearchQuery { get; }
    }

    public abstract class SelectProxyBase<TModel> : ProxyBase<TModel>, ISelectProxyBase<TModel>
        where TModel : class, IModel, new()
    {
        protected SelectProxyBase()
        {
            SearchQuery = new SearchQuery<TModel>();
        }

        protected SelectProxyBase(ISearchQuery<TModel> searchQuery)
        {
            SearchQuery = searchQuery ?? throw new ArgumentNullException(nameof(searchQuery));
        }

        public ISearchQuery<TModel> SearchQuery { get; }

        ISearchQuery ISelectProxyBase.SearchQuery => SearchQuery;
    }
}