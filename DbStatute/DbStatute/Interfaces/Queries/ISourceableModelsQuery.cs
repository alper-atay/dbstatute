using System.Collections.Generic;

namespace DbStatute.Interfaces.Queries
{
    public interface ISourceableModelsQuery
    {
        IEnumerable<object> SourceModels { get; }
    }

    public interface ISourceableModelsQuery<TModel> : ISourceableModelsQuery
    where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> SourceModels { get; }
    }
}