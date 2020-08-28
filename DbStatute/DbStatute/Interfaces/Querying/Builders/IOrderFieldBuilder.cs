using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IOrderFieldBuilder
    {
        IOrderFieldQualifier OrderFieldQualifier { get; }

        bool BuildOrderFields(out IEnumerable<OrderField> orderFields);
    }

    public interface IOrderFieldBuilder<TModel> : IOrderFieldBuilder
        where TModel : class, IModel, new()
    {

    }
}