using Basiclog;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Interfaces.Singles;
using DbStatute.Querying.Builders;
using DbStatute.Querying.Qualifiers;
using DbStatute.Querying.Qualifiers.Fields;
using RepoDb;
using RepoDb.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier> : SingleInsertBase<TModel>, ISingleInsertByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : class, IFieldQualifier<TModel>
        where TPredicateFieldQualifier : class, IPredicateFieldQualifier<TModel>
    {
        public SingleInsertByRawModel(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = new FieldQualifier<TModel>() as TFieldQualifier;
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>() as TPredicateFieldQualifier;
        }

        public SingleInsertByRawModel(TModel rawModel, TFieldQualifier fieldQualifier)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>() as TPredicateFieldQualifier;
        }

        public SingleInsertByRawModel(TModel rawModel, TFieldQualifier fieldQualifier, TPredicateFieldQualifier predicateFieldQualifier)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public TFieldQualifier FieldQualifier { get; }
        public TPredicateFieldQualifier PredicateFieldQualifier { get; }
        public TModel RawModel { get; }
        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            IFieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(FieldQualifier);
            Logs.AddRange(fieldBuilder.ReadOnlyLogs);

            if (fieldBuilder.Build(out IEnumerable<Field> fields))
            {
                TPredicateFieldQualifier predicateFieldQualifier = PredicateFieldQualifier;
                IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> fieldPredicateMap = predicateFieldQualifier.ReadOnlyFieldPredicateMap;

                Type modelType = typeof(TModel);

                foreach (Field field in fields)
                {
                    if (fieldPredicateMap.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate))
                    {
                        if (predicate != null)
                        {
                            PropertyInfo modelProperty = modelType.GetProperty(field.Name);

                            if (modelProperty is null)
                            {
                                throw new PropertyNotFoundException($"{field.Name} named property could not found in {modelType.FullName}");
                            }

                            object value = modelProperty.GetValue(RawModel);

                            Logs.AddRange(predicate.Invoke(value));
                        }
                    }
                }
            }

            TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(RawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            return insertedModel;
        }
    }
}