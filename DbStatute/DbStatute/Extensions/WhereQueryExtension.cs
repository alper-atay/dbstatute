using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Queries;
using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;

namespace DbStatute.Extensions
{
    public static class WhereQueryExtension
    {
        public static IReadOnlyLogbook Build<TModel>(this IWhereQuery<TModel> whereQuery, Conjunction conjunction, out QueryGroup queryGroup) where TModel : class, IModel, new()
        {
            queryGroup = null;

            ILogbook logs = Logger.NewLogbook();

            logs.AddRange(QueryFieldExtension.Build<TModel>(whereQuery.FieldQualifier, whereQuery.ValueFieldQualifier, whereQuery.PredicateFieldQualifier, whereQuery.OperationFieldQualifier, out IEnumerable<QueryField> queryFields));

            if (logs.Safely)
            {
                queryGroup = new QueryGroup(queryFields, conjunction);
            }

            return logs;
        }
    }
}