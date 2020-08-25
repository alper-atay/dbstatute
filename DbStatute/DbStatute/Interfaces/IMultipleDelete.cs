using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleDelete<TId, TModel, TMultipleSelect>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TMultipleSelect : IMultipleSelect<TId, TModel>
    {
        TMultipleSelect MultipleSelect { get; }

        Task<int> DeleteAsync(IDbConnection dbConnection);
    }
}