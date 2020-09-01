using Basiclog;
using DbStatute.Builders;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using RepoDb.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbStatute.Internals
{
    internal static class RawModelHelper
    {
        public static IReadOnlyLogbook PredicateModel<TModel>(TModel model, IEnumerable<Field> fields, IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> fieldPredicateMap) where TModel : class, IModel, new()
        {
            ILogbook logs = Logger.NewLogbook();

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

                        object value = modelProperty.GetValue(model);

                        logs.AddRange(predicate.Invoke(value));
                    }
                }
            }

            return logs;
        }

        public static IReadOnlyLogbook PredicateModel<TModel>(TModel model, IFieldQualifier<TModel> fieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier, out IEnumerable<Field> fields) where TModel : class, IModel, new()
        {
            ILogbook logs = Logger.NewLogbook();

            FieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(fieldQualifier);
            fieldBuilder.Build(out fields);
            logs.AddRange(fieldBuilder.ReadOnlyLogs);

            if (logs.Safely)
            {
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

                            object value = modelProperty.GetValue(model);

                            logs.AddRange(predicate.Invoke(value));
                        }
                    }
                }
            }

            return logs;
        }

        public static IReadOnlyLogbook PredicateModels<TModel>(IEnumerable<TModel> models, IEnumerable<Field> fields, IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> fieldPredicateMap) where TModel : class, IModel, new()
        {
            ILogbook logs = Logger.NewLogbook();

            Type modelType = typeof(TModel);

            foreach (TModel model in models)
            {
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

                            object value = modelProperty.GetValue(model);

                            logs.AddRange(predicate.Invoke(value));
                        }
                    }
                }
            }

            return logs;
        }
    }
}