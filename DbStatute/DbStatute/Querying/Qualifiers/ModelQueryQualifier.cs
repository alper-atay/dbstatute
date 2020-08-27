using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using RepoDb.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbStatute.Querying.Qualifiers
{
    public class ModelQueryQualifier<TModel> : QueryQualifier<TModel>, IModelQueryQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public IReadOnlyLogbook GetModel(out TModel model)
        {
            model = null;

            ILogbook logs = Logger.NewLogbook();
            logs.AddRange(Test());

            if (logs.Safely)
            {
                model = new TModel();

                bool hasField = FieldQualifier.HasField;
                if (hasField)
                {
                    Type modelType = typeof(TModel);
                    IEnumerable<Field> fields = FieldQualifier.Fields;

                    foreach (Field field in fields)
                    {
                        string name = field.Name;

                        PropertyInfo propertyInfo = modelType.GetProperty(name);
                        if (propertyInfo is null)
                        {
                            throw new PropertyNotFoundException($"{field.Name} named property could not found in ${modelType.FullName}");
                        }

                        propertyInfo.SetValue(model, GetValueOrDefault(name));
                    }
                }
            }

            return logs;
        }

        public IReadOnlyLogbook GetModel(out object model)
        {
            return GetModel(out model);
        }
    }
}