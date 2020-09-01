using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Querying.Qualifiers.Fields;
using RepoDb;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbStatute.Querying.Builders
{
    public class QueryGroupBuilder<TModel> : Builder<QueryGroup>, IQueryGroupBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public IFieldQualifier<TModel> FieldQualifier { get; }
        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
        IFieldQualifier IQueryGroupBuilder.FieldQualifier => FieldQualifier;
        IPredicateFieldQualifier IQueryGroupBuilder.PredicateFieldQualifier => PredicateFieldQualifier;
        IValueFieldQualifier IQueryGroupBuilder.ValueFieldQualifier => ValueFieldQualifier;

        public QueryGroupBuilder()
        {
            FieldQualifier = new FieldQualifier<TModel>();
            ValueFieldQualifier = new ValueFieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public QueryGroupBuilder(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            FieldQualifier = fieldQualifier;
            ValueFieldQualifier = valueFieldQualifier;
            PredicateFieldQualifier = predicateFieldQualifier;
        }

        protected override bool BuildOperation(out QueryGroup built)
        {
            built = null;

            FieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(FieldQualifier);

            if (fieldBuilder.Build(out IEnumerable<Field> fields))
            {
                Logs.AddRange(fieldBuilder.ReadOnlyLogs);

                IReadOnlyDictionary<Field, object> valueMap = ValueFieldQualifier.ReadOnlyFieldValueMap;
                IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> predicateMap = PredicateFieldQualifier.ReadOnlyFieldPredicateMap;

                ICollection<QueryField> queryFields = new Collection<QueryField>();

                foreach (Field field in fields)
                {
                    bool valueFound = valueMap.TryGetValue(field, out object value);
                    bool predicateFound = predicateMap.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate);

                    if (valueFound && predicateFound && predicate != null)
                    {
                        Logs.AddRange(predicate.Invoke(value));
                    }

                    if (valueFound)
                    {
                        queryFields.Add(new QueryField(field, value));
                    }
                }

                if (ReadOnlyLogs.Safely)
                {
                    built = new QueryGroup(queryFields);

                    return true;
                }
            }

            return false;
        }
    }
}