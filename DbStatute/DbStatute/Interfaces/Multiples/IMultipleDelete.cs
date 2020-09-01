﻿using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDelete : IMultipleDeleteBase, IReadyModels
    {
    }

    public interface IMultipleDelete<TModel> : IMultipleDeleteBase<TModel>, IReadyModels<TModel>, IMultipleDelete
        where TModel : class, IModel, new()
    {
    }
}