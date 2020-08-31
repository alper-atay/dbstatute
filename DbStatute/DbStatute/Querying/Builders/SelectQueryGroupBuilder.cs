using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbStatute.Querying.Builders
{
    public class SelectQueryGroupBuilder<TModel> : Builder<QueryGroup>, ISelectQueryGroupBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public Conjunction Conjunction { get; set; } = Conjunction.And;
        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IQueryGroupBuilder.FieldQualifier => FieldQualifier;
        public IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }
        IOperationFieldQualifier ISelectQueryGroupBuilder.OperationFieldQualifier => OperationFieldQualifier;
        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        IPredicateFieldQualifier IQueryGroupBuilder.PredicateFieldQualifier => PredicateFieldQualifier;
        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
        IValueFieldQualifier IQueryGroupBuilder.ValueFieldQualifier => ValueFieldQualifier;

        protected override bool BuildOperation(out QueryGroup built)
        {
            built = null;

            FieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(FieldQualifier);

            if (fieldBuilder.Build(out IEnumerable<Field> fields))
            {
                IValueFieldQualifier<TModel> valueFieldQualifier = ValueFieldQualifier;
                IReadOnlyDictionary<Field, object> valueMap = valueFieldQualifier.ReadOnlyFieldValueMap;

                IPredicateFieldQualifier<TModel> predicateFieldQualifier = PredicateFieldQualifier;
                IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> predicateMap = predicateFieldQualifier.ReadOnlyFieldPredicateMap;

                IOperationFieldQualifier<TModel> operationFieldQualifier = OperationFieldQualifier;
                IReadOnlyDictionary<Field, Operation> operationMap = operationFieldQualifier.ReadOnlyFieldOperationMap;

                ICollection<QueryField> queryFields = new Collection<QueryField>();

                foreach (Field field in fields)
                {
                    bool valueFound = valueMap.TryGetValue(field, out object value);
                    bool predicateFound = predicateMap.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate);
                    bool operationFound = operationMap.TryGetValue(field, out Operation operation);

                    if (valueFound && predicateFound && predicate != null)
                    {
                        Logs.AddRange(predicate.Invoke(value));
                    }

                    if (!ReadOnlyLogs.Safely)
                    {
                        break;
                    }

                    if (valueFound && operationFound)
                    {
                        queryFields.Add(new QueryField(field, operation, value));
                    }
                }

                if (ReadOnlyLogs.Safely)
                {
                    built = new QueryGroup(queryFields, Conjunction);
                }
            }

            return !(built is null);
        }
    }
}