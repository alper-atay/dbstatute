using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Queries;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbStatute.Extensions
{
    public static class ModelQueryExtension
    {
        public static IReadOnlyLogbook Build<TModel>(this IModelQuery<TModel> modelQuery, out TModel model) where TModel : class, IModel, new()
        {
            model = null;

            ILogbook logs = Logger.NewLogbook();

            if (modelQuery.FieldQualifier.Build<TModel>(out IEnumerable<Field> fields))
            {
                IReadOnlyDictionary<Field, object> valueMap = modelQuery.ValueFieldQualifier;
                IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> predicateMap = modelQuery.PredicateFieldQualifier;

                foreach (Field field in fields)
                {
                    bool valueFound = valueMap.TryGetValue(field, out object value);
                    bool predicateFound = predicateMap.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate);

                    if (valueFound && predicateFound && predicate != null)
                    {
                        logs.AddRange(predicate.Invoke(value));
                    }
                }

                if (logs.Safely)
                {
                    model = new TModel();

                    Type modelType = typeof(TModel);
                    IEnumerable<Field> modelFields = Field.Parse<TModel>();

                    foreach (Field modelField in modelFields)
                    {
                        PropertyInfo propertyField = modelType.GetProperty(modelField.Name);
                        propertyField.SetValue(model, valueMap[modelField]);
                    }
                }
            }

            return logs;
        }
    }
}