using System.Collections.Generic;

namespace DbStatute.Interfaces
{
    public interface ISourceModels
    {
        IEnumerable<object> SourceModels { get; }
    }

    public interface ISourceModels<TModel> : ISourceModels
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> SourceModels { get; }
    }
}