using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using RepoDb.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace DbStatute.Querying.Builders
{
    public class ModelBuilder<TModel> : Builder<TModel>, IModelBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IModelBuilder.FieldQualifier => FieldQualifier;
        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        IPredicateFieldQualifier IModelBuilder.PredicateFieldQualifier => PredicateFieldQualifier;
        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
        IValueFieldQualifier IModelBuilder.ValueFieldQualifier => ValueFieldQualifier;

        /// <summary>
        /// Create new predicated model from qualifiers
        /// </summary>
        /// <param name="built"></param>
        /// <returns></returns>
        protected override bool BuildOperation(out TModel built)
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
                }

                if (!ReadOnlyLogs.Safely)
                {
                    return false;
                }

                HashSet<Field> modelFields = Field.Parse<TModel>().ToHashSet();
                Type modelType = typeof(TModel);

                if (!modelFields.IsSupersetOf(fields))
                {
                    throw new InvalidTypeException($"{modelType.FullName} not superset of field qualifier");
                }

                built = new TModel();

                foreach (Field modelField in modelFields)
                {
                    PropertyInfo propertyField = modelType.GetProperty(modelField.Name);
                    propertyField.SetValue(built, valueMap[modelField]);
                }
            }

            return true;
        }
    }
}