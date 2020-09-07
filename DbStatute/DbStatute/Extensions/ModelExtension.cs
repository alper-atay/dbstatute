using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbStatute.Extensions
{
    public static class ModelExtension
    {
        public static IEnumerable<object> GetIds(this IEnumerable<IModel> models)
        {
            return models.Select(x => x.Id);
        }

        public static QueryField GetIdsInQuery(this IEnumerable<IModel> models)
        {
            IEnumerable<object> modelIds = GetIds(models);

            return new QueryField(nameof(IModel.Id), Operation.In, modelIds);
        }

        public static IReadOnlyLogbook Predicate<TModel>(this TModel model, IEnumerable<Field> fields, IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> predicates) where TModel : class, IModel, new()
        {
            ILogbook logs = Logger.NewLogbook();

            Type modelType = typeof(TModel);

            foreach (Field field in fields)
            {
                if (predicates.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate))
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

        public static IReadOnlyLogbook Predicate<TModel>(this TModel model, IFieldQualifier<TModel> fieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier, out IEnumerable<Field> fields) where TModel : class, IModel, new()
        {
            ILogbook logs = Logger.NewLogbook();

            if (fieldQualifier.Build<TModel>(out fields))
            {
                IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> fieldPredicateMap = predicateFieldQualifier;

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
            else
            {
                Type modelType = typeof(TModel);

                foreach (KeyValuePair<Field, ReadOnlyLogbookPredicate<object>> fieldPredicatePair in predicateFieldQualifier)
                {
                    Field field = fieldPredicatePair.Key;
                    ReadOnlyLogbookPredicate<object> predicate = fieldPredicatePair.Value;

                    if (predicate is null)
                    {
                        continue;
                    }

                    PropertyInfo modelProperty = modelType.GetProperty(field.Name);
                    if (modelProperty is null)
                    {
                        throw new PropertyNotFoundException($"{field.Name} named property could not found in {modelType.FullName}");
                    }

                    object value = modelProperty.GetValue(model);

                    logs.AddRange(predicate.Invoke(value));
                }
            }

            return logs;
        }

        public static IReadOnlyLogbook PredicateAll<TModel>(IEnumerable<TModel> models, IEnumerable<Field> fields, IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> fieldPredicateMap) where TModel : class, IModel, new()
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