using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Enumerables;
using RepoDb;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Extensions
{
    public static class FieldCollectionExtension
    {
        public static bool Build(this IFieldCollection fieldCollection, out IEnumerable<Field> fields)
        {
            fields = Enumerable.Empty<Field>();

            if (fieldCollection.HasField)
            {
                fields = fieldCollection;

                return true;
            }

            return false;
        }

        public static bool Build<TModel>(this IFieldCollection fieldCollection, out IEnumerable<Field> fields) where TModel : class, IModel, new()
        {
            fields = Enumerable.Empty<Field>();

            if (fieldCollection.HasField && fieldCollection.IsSubsetOfModel<TModel>())
            {
                fields = fieldCollection;

                return true;
            }

            return false;
        }

        public static bool IsSubsetOfModel<TModel>(this IEnumerable<Field> fields) where TModel : class, IModel, new()
        {
            HashSet<Field> modelFieldHashSet = Field.Parse<TModel>().ToHashSet();
            HashSet<Field> fieldHashSet = fields.ToHashSet();

            return fieldHashSet.IsSubsetOf(modelFieldHashSet);
        }
    }
}