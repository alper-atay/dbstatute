using Basiclog;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IQueryGroupBuilder : IQueryQualifier
    {
        IReadOnlyLogbook BuildQuery(out QueryGroup queryGroup);

        protected static IReadOnlyLogbook BuildQuery(IQueryQualifier @this, out QueryGroup queryGroup)
        {
            queryGroup = null;
            ILogbook logs = Logger.NewLogbook();

            IFieldQualifier fieldQualifier = @this.FieldQualifier;
            IEnumerable<Field> fields = fieldQualifier.Fields;

            ICollection<QueryField> queryFields = new Collection<QueryField>();

            foreach (Field field in fields)
            {
                string name = field.Name;
                bool valueFound = @this.FieldValueMap.TryGetValue(name, out object value);
                bool predicateFound = @this.FieldPredicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate);

                if (valueFound)
                {
                    QueryField queryField = new QueryField(field, value);
                    queryFields.Add(queryField);
                }

                if (valueFound && predicateFound)
                {
                    logs.AddRange(predicate.Invoke(value));
                }
            }

            int queryFieldCount = queryFields.Count;

            if (queryFieldCount > 0)
            {
                queryGroup = new QueryGroup(queryFields);
            }

            return logs;
        }
    }
}
