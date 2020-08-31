using DbStatute.Interfaces.Utilities;
using DbStatute.Querying.Qualifiers.Fields;
using RepoDb;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Interfaces.Querying.Qualifiers.Fields
{
    public interface IOrderFieldQualifier : ISettableOrderField
    {
        bool HasOrderField { get; }
        IEnumerable<OrderField> OrderFields { get; }
    }

    public interface IOrderFieldQualifier<TModel> : ISettableOrderField<TModel>, IOrderFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}