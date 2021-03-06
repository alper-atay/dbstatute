﻿namespace DbStatute.Interfaces.Fundamentals
{
    public interface ISelectBase : IStatuteBase
    {
        public ICacheable Cacheable { get; set; }

        int? MaxSelectCount { get; }

        int SelectedCount { get; }
    }

    public interface ISelectBase<TModel> : IStatuteBase<TModel>, ISelectBase
        where TModel : class, IModel, new()
    {
    }
}