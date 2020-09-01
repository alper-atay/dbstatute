using System.Collections.Generic;

namespace DbStatute.Interfaces
{
    public interface IReadyModels
    {
        IEnumerable<object> ReadyModels { get; }
    }

    public interface IReadyModels<TModel> : IReadyModels
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> ReadyModels { get; }
    }
}