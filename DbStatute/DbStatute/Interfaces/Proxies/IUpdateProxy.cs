﻿using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxy : IProxyBase
    {
        IFieldQualifier FieldQualifier { get; }

        IModelBuilder ModelBuilder { get; }
    }

    public interface IUpdateProxy<TModel> : IProxyBase<TModel>, IUpdateProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IModelBuilder<TModel> ModelBuilder { get; }
    }
}