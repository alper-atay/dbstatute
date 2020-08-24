using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleSelect<TId, TModel> : ISelect
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        TModel SelectedModel { get; }

        Task<TModel> SelectAsync(IDbConnection dbConnection);
    }
}