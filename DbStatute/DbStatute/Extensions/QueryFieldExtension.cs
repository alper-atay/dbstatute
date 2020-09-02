using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Enumerables;
using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DbStatute.Extensions
{
    public static class QueryFieldExtension
    {
        public static IReadOnlyLogbook CreateQueryFields<TModel>(IFieldCollection fields, IFieldValuePairs fieldValuePairs, IFieldPredicatePairs fieldPredicatePairs, out IEnumerable<QueryField> queryFields) where TModel : class, IModel, new()
        {
            queryFields = Enumerable.Empty<QueryField>();

            ILogbook logs = Logger.NewLogbook();
            if (!fields.IsSubsetOfModel<TModel>())
            {
                logs.Failure("Qualifier fields must be subset of model fields");

                return logs;
            }

            ICollection<QueryField> queryFieldCollection = new Collection<QueryField>();

            foreach (Field field in fields)
            {
                bool valueFound = fieldValuePairs.TryGetValue(field, out object value);
                bool predicateFound = fieldPredicatePairs.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate);

                if (valueFound && predicateFound && predicate != null)
                {
                    logs.AddRange(predicate.Invoke(value));
                }

                if (!logs.Safely)
                {
                    break;
                }
            }

            return logs;
        }

        public static IReadOnlyLogbook CreateQueryFields(IFieldCollection fields, IFieldValuePairs fieldValuePairs, IFieldPredicatePairs fieldPredicatePairs, IFieldOperationPairs fieldOperationPairs, out IEnumerable<QueryField> queryFields)
        {
            queryFields = Enumerable.Empty<QueryField>();

            ILogbook logs = Logger.NewLogbook();

            ICollection<QueryField> queryFieldCollection = new Collection<QueryField>();

            foreach (Field field in fields)
            {
                bool valueFound = fieldValuePairs.TryGetValue(field, out object value);
                bool predicateFound = fieldPredicatePairs.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate);
                bool operationFound = fieldOperationPairs.TryGetValue(field, out Operation operation);

                if (valueFound && predicateFound && predicate != null)
                {
                    logs.AddRange(predicate.Invoke(value));
                }

                if (!logs.Safely)
                {
                    break;
                }

                if (valueFound && operationFound)
                {
                    queryFieldCollection.Add(new QueryField(field, operation, value));
                }
            }

            return logs;
        }

        public static IReadOnlyLogbook CreateQueryFields<TModel>(IFieldCollection fields, IFieldValuePairs fieldValuePairs, IFieldPredicatePairs fieldPredicatePairs, IFieldOperationPairs fieldOperationPairs, out IEnumerable<QueryField> queryFields) where TModel : class, IModel, new()
        {
            queryFields = Enumerable.Empty<QueryField>();

            ILogbook logs = Logger.NewLogbook();

            if (!fields.IsSubsetOfModel<TModel>())
            {
                logs.Failure("Qualifier fields must be subset of model fields");

                return logs;
            }

            ICollection<QueryField> queryFieldCollection = new Collection<QueryField>();

            foreach (Field field in fields)
            {
                bool valueFound = fieldValuePairs.TryGetValue(field, out object value);
                bool predicateFound = fieldPredicatePairs.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate);
                bool operationFound = fieldOperationPairs.TryGetValue(field, out Operation operation);

                if (valueFound && predicateFound && predicate != null)
                {
                    logs.AddRange(predicate.Invoke(value));
                }

                if (!logs.Safely)
                {
                    break;
                }

                if (valueFound && operationFound)
                {
                    queryFieldCollection.Add(new QueryField(field, operation, value));
                }
            }

            return logs;
        }
    }
}