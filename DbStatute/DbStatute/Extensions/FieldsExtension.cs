using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Enumerables;
using RepoDb;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Extensions
{
    public static class FieldsExtension
    {
        public static bool IsSubsetOfModel<TModel>(this IFields fields) where TModel : class, IModel, new()
        {
            HashSet<Field> modelFieldHashSet = Field.Parse<TModel>().ToHashSet();
            HashSet<Field> fieldHashSet = fields.Fields.ToHashSet();

            return fieldHashSet.IsSubsetOf(modelFieldHashSet);
        }
    }
}