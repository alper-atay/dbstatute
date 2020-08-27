using System.Collections.Generic;

namespace DbStatute.Interfaces
{
    public interface IRawModels
    {
        IEnumerable<object> RawModels { get; }
    }

    public interface IRawModels<TModel> : IRawModels
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> RawModels { get; }
    }
}