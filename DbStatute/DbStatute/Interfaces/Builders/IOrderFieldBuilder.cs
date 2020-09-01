using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Builders
{
    public interface IOrderFieldBuilder : IBuilder
    {
        IOrderFieldQualifier OrderFieldQualifier { get; }
    }

    public interface IOrderFieldBuilder<TModel> : IBuilder<IEnumerable<OrderField>>, IOrderFieldBuilder
        where TModel : class, IModel, new()
    {
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
    }
}
