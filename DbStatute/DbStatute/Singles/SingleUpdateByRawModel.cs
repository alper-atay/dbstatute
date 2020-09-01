﻿using Basiclog;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Interfaces.Singles;
using DbStatute.Querying.Builders;
using RepoDb;
using RepoDb.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdateByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier> : SingleUpdateBase<TModel>, ISingleUpdateByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : class, IFieldQualifier<TModel>
        where TPredicateFieldQualifier : class, IPredicateFieldQualifier<TModel>
    {
        public SingleUpdateByRawModel(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
        }

        public SingleUpdateByRawModel(TModel rawModel, TFieldQualifier fieldQualifier, TPredicateFieldQualifier predicateFieldQualifier)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public TFieldQualifier FieldQualifier { get; }
        public TPredicateFieldQualifier PredicateFieldQualifier { get; }
        public TModel RawModel { get; }
        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            FieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(FieldQualifier);
            fieldBuilder.Build(out IEnumerable<Field> fields);
            Logs.AddRange(fieldBuilder.ReadOnlyLogs);

            if (ReadOnlyLogs.Safely)
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

                int updatedId = await dbConnection.UpdateAsync(RawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                return await dbConnection.QueryAsync<TModel>(updatedId, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            return null;
        }
    }
}