using Basiclog;
using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IQueryQualifier : IReadOnlyLogbookTestable, IFieldPredicateMap, IFieldValueMap
    {
        IFieldQualifier FieldQualifier { get; }

        protected static IReadOnlyLogbook Test(IQueryQualifier @this)
        {
            ILogbook logs = Logger.NewLogbook();

            IFieldQualifier fieldQualifier = @this.FieldQualifier;
            IEnumerable<Field> fields = fieldQualifier.Fields;

            foreach (Field field in fields)
            {
                string name = field.Name;
                bool valueFound = @this.FieldValueMap.TryGetValue(name, out object value);
                bool predicateFound = @this.FieldPredicateMap.TryGetValue(name, out ReadOnlyLogbookPredicate<object> predicate);

                if (valueFound && predicateFound)
                {
                    logs.AddRange(predicate.Invoke(value));
                }
            }

            return logs;
        }
    }

    public interface IQueryQualifier<TModel> : IQueryQualifier
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        ReadOnlyLogbookPredicate<object> GetPredicateOrDefault(Expression<Func<TModel, object>> expression);

        ReadOnlyLogbookPredicate<object> GetPredicateOrDefault(string name);

        object GetValueOrDefault(Expression<Func<TModel, object>> expression);

        object GetValueOrDefault(string name);



    }
}