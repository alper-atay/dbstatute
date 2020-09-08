using Basiclog;
using DbStatute.Extensions;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Helpers
{
    public static class FieldHelper
    {
        public static IReadOnlyLogbook Predicate<TModel>(IFieldQualifier<TModel> fieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier) where TModel : class, IModel, new()
        {
            if (fieldQualifier.Build<TModel>(out IEnumerable<Field> fields))
            {
            }

            return null;
        }
    }
}