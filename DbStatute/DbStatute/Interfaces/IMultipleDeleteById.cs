using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleDeleteById<TId, TModel, TMultipleSelectById>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TMultipleSelectById : IMultipleSelectById<TId, TModel>
    {
        TMultipleSelectById MultipleSelectById { get; }

        Task<int> DeleteAsync(IDbConnection dbConnection);
    }
}