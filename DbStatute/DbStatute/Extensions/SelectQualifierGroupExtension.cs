using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers.Groups;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Extensions
{
    public static class SelectQualifierGroupExtension
    {
        public static IReadOnlyLogbook Build(this ISelectQualifierGroup selectQualifierGroup, out QueryGroup queryGroup)
        {
            queryGroup = null;

            ILogbook logs = Logger.NewLogbook();

            logs.AddRange(QueryFieldExtension.Build(selectQualifierGroup.FieldQualifier, selectQualifierGroup.ValueFieldQualifier, selectQualifierGroup.PredicateFieldQualifier, selectQualifierGroup.OperationFieldQualifier, out IEnumerable<QueryField> queryFields));

            if (logs.Safely)
            {
                queryGroup = new QueryGroup(queryFields, selectQualifierGroup.Conjunction);
            }

            return logs;
        }

        public static IReadOnlyLogbook Build<TModel>(this ISelectQualifierGroup selectQualifierGroup, out QueryGroup queryGroup) where TModel : class, IModel, new()
        {
            queryGroup = null;

            ILogbook logs = Logger.NewLogbook();

            logs.AddRange(QueryFieldExtension.Build<TModel>(selectQualifierGroup.FieldQualifier, selectQualifierGroup.ValueFieldQualifier, selectQualifierGroup.PredicateFieldQualifier, selectQualifierGroup.OperationFieldQualifier, out IEnumerable<QueryField> queryFields));

            if (logs.Safely)
            {
                queryGroup = new QueryGroup(queryFields, selectQualifierGroup.Conjunction);
            }

            return logs;
        }
    }
}