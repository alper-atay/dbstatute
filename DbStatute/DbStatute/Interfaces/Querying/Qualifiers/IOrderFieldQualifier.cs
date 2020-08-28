using DbStatute.Interfaces.Utilities;
using DbStatute.Querying.Qualifiers.Fields;
using RepoDb;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IOrderFieldQualifier : ISettableOrderField
    {
        private static readonly IOrderFieldQualifier _empty;

        static IOrderFieldQualifier()
        {
            _empty = new OrderFieldQualifier(Enumerable.Empty<OrderField>());
        }

        static IOrderFieldQualifier Empty => _empty;

        bool HasOrderField { get; }
        IEnumerable<OrderField> OrderFields { get; }
    }

    public interface IOrderFieldQualifier<TModel> : ISettableOrderField<TModel>, IOrderFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}