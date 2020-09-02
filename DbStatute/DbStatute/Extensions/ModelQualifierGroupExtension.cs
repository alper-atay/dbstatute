using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers.Groups;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbStatute.Extensions
{
    public static class ModelQualifierGroupExtension
    {
        public static IReadOnlyLogbook Build<TModel>(this IModelQualifierGroup modelQualifierGroup, out TModel model) where TModel : class, IModel, new()
        {
            model = null;

            ILogbook logs = Logger.NewLogbook();

            if (modelQualifierGroup.FieldQualifier.Build<TModel>(out IEnumerable<Field> fields))
            {
                IReadOnlyDictionary<Field, object> valueMap = modelQualifierGroup.ValueFieldQualifier;
                IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> predicateMap = modelQualifierGroup.PredicateFieldQualifier;

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