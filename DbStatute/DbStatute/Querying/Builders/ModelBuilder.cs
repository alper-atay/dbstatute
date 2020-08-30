using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Querying.Builders
{
    public class ModelBuilder : IModelBuilder
    {
        public ModelBuilder(IFieldBuilder fieldBuilder)
        {
            FieldBuilder = fieldBuilder ?? throw new ArgumentNullException(nameof(fieldBuilder));
        }

        public IFieldBuilder FieldBuilder { get; }

        public IPredicateFieldQualifier PredicateFieldQualifier => throw new NotImplementedException();
        public IValueFieldQualifier ValueFieldQualifier => throw new NotImplementedException();

        public bool Build(out dynamic model)
        {
            model = null;

            if (FieldBuilder.Build(out IEnumerable<Field> fields))
            {
                // Need to How to dynamic new Anonymous Class?
                // https://stackoverflow.com/questions/3740021/how-to-dynamic-new-anonymous-class

                throw new NotImplementedException();

                //turn true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ModelBuilder<TModel> : IModelBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public IFieldBuilder<TModel> FieldBuilder { get; }
        IFieldBuilder IModelBuilder.FieldBuilder => FieldBuilder;
        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        IPredicateFieldQualifier IModelBuilder.PredicateFieldQualifier => PredicateFieldQualifier;
        public IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
        IValueFieldQualifier IModelBuilder.ValueFieldQualifier => ValueFieldQualifier;

        public bool Build(out TModel model)
        {
            model = null;

            if (FieldBuilder.Build(out IEnumerable<Field> builtFields))
            {
                HashSet<Field> modelFields = Field.Parse<TModel>().ToHashSet();
                HashSet<Field> fields = builtFields.ToHashSet();

                if (modelFields.IsSupersetOf(fields))
                {
                    model = new TModel();
                    Type modelType = typeof(TModel);

                    foreach (Field field in fields)
                    {
                        bool predicateFound = PredicateFieldQualifier.FieldPredicateMap.TryGetValue(field, out ReadOnlyLogbookPredicate<object> predicate);
                        bool valueFound = ValueFieldQualifier.FieldValueMap.TryGetValue(field, out object value);

                        if (valueFound && predicateFound && predicate != null)
                        {

                        }

                        if (valueFound)
                        {

                        }

                        //PropertyInfo fieldProperty = modelType.GetProperty(field.Name);

                        //if (fieldProperty is null)
                        //{
                        //    throw new PropertyNotFoundException($"{field.Name} property not found in {modelType.FullName}");
                        //}



                        //fieldProperty.SetValue(model, )
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Build(out dynamic model)
        {
            throw new NotImplementedException();
        }
    }
}