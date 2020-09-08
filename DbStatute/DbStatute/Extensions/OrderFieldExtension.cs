using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Enumerables;
using RepoDb;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Extensions
{
    public static class OrderFieldExtension
    {
        public static bool Build(this IOrderFieldCollection orderFieldCollection, out IEnumerable<OrderField> orderFields)
        {
            orderFields = orderFieldCollection;

            return true;
        }

        public static bool Build<TModel>(this IOrderFieldCollection orderFieldCollection, out IEnumerable<OrderField> orderFields) where TModel : class, IModel, new()
        {
            orderFields = Enumerable.Empty<OrderField>();

            IEnumerable<Field> fields = Field.From(orderFieldCollection.Select(x => x.Name).ToArray());

            if (fields.IsSubsetOfModel<TModel>())
            {
                orderFields = orderFieldCollection;

                return true;
            }

            return false;
        }
    }
}