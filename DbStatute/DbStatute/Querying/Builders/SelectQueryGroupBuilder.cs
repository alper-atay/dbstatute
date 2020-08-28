using Basiclog;
using DbStatute.Enumerations;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Querying.Qualifiers.Fields;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbStatute.Querying.Builders
{
    public class SelectQueryGroupBuilder : QueryGroupBuilder, ISelectQueryGroupBuilder
    {
        public SelectQueryGroupBuilder(IFieldBuilder fieldBuilder) : base(QueryGroupUsage.Select, fieldBuilder)
        {
            OperationFieldQualifier = new OperationFieldQualifier();
        }

        public SelectQueryGroupBuilder(IFieldBuilder fieldBuilder, IValueFieldQualifier valueFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier, IOperationFieldQualifier operationFieldQualifier) : base(QueryGroupUsage.Select, fieldBuilder, valueFieldQualifier, predicateFieldQualifier)
        {
            OperationFieldQualifier = operationFieldQualifier ?? throw new ArgumentNullException(nameof(operationFieldQualifier));
        }

        public Conjunction Conjunction { get; set; } = Conjunction.And;

        public IOperationFieldQualifier OperationFieldQualifier { get; }

        public override IReadOnlyLogbook Build(out QueryGroup queryGroup)
        {
            queryGroup = null;
            ILogbook logs = Logger.NewLogbook();

            IFieldBuilder fieldBuilder = FieldBuilder;

            if (fieldBuilder.Build(out IEnumerable<Field> fields))
            {
                IValueFieldQualifier valueFieldQualifier = ValueFieldQualifier;
                IReadOnlyDictionary<Field, object> valueMap = valueFieldQualifier.FieldValueMap;

                IPredicateFieldQualifier predicateFieldQualifier = PredicateFieldQualifier;
                IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> predicateMap = predicateFieldQualifier.FieldPredicateMap;

                IOperationFieldQualifier operationFieldQualifier = OperationFieldQualifier;
                IReadOnlyDictionary<Field, Operation> operationMap = operationFieldQualifier.FieldOperationMap;

                ICollection<QueryField> queryFields = new Collection<QueryField>();

                foreach (Field field in fields)
                {
                    bool valueFound = valueMap.TryGetValue(field, out object value);
                    bool predicateFound = predicateMap.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate);
                    bool operationFound = operationMap.TryGetValue(field, out Operation operation);

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
                        queryFields.Add(new QueryField(field, operation, value));
                    }
                }
            }

            return logs;
        }
    }

    public class SelectQueryGroupBuilder<TModel> : QueryGroupBuilder<TModel>, ISelectQueryGroupBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public SelectQueryGroupBuilder(IFieldBuilder fieldBuilder) : base(QueryGroupUsage.Select, fieldBuilder)
        {
            OperationFieldQualifier = new OperationFieldQualifier<TModel>();
        }

        public SelectQueryGroupBuilder(IFieldBuilder fieldBuilder, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier, IOperationFieldQualifier<TModel> operationFieldQualifier) : base(QueryGroupUsage.Select, fieldBuilder, valueFieldQualifier, predicateFieldQualifier)
        {
            OperationFieldQualifier = operationFieldQualifier ?? throw new ArgumentNullException(nameof(operationFieldQualifier));
        }

        public Conjunction Conjunction { get; set; }
        public IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }
        IOperationFieldQualifier ISelectQueryGroupBuilder.OperationFieldQualifier => OperationFieldQualifier;

        public override IReadOnlyLogbook Build(out QueryGroup queryGroup)
        {
            throw new NotImplementedException();
        }
    }
}