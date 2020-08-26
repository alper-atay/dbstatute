using System.Collections.Generic;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelectById : IMultipleSelect
    {
        IEnumerable<object> Ids { get; }
    }


    public interface IMultipleSelectById<TModel> : IMultipleSelect<TModel>, IMultipleSelectById
        where TModel : class, IModel, new()
    {
    }
}