using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Queries;
using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;

namespace DbStatute.Extensions
{
    public static class SearchQueryExtension
    {
        public static IReadOnlyLogbook Build<TModel>(this ISearchQuery<TModel> searchQuery, Conjunction conjunction, out QueryGroup queryGroup) where TModel : class, IModel, new()
        {
            queryGroup = null;

            ILogbook logs = Logger.NewLogbook();

            logs.AddRange(QueryFieldExtension.Build<TModel>(searchQuery.Fields, searchQuery.ValueMap, searchQuery.PredicateMap, searchQuery.OperationMap, out IEnumerable<QueryField> queryFields));

            if (logs.Safely)
            {
                queryGroup = new QueryGroup(queryFields, conjunction);
            }

            return logs;
        }
    }
}