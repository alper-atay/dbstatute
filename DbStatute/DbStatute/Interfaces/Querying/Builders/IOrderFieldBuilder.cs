using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IOrderFieldBuilder
    {
        IOrderFieldQualifier OrderFieldQualifier { get; }

        bool Build(out IEnumerable<OrderField> orderFields);
    }

    public interface IOrderFieldBuilder<TModel> : IOrderFieldBuilder
        where TModel : class, IModel, new()
    {
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
    }
}