using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelect<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        IEnumerable<TModel> SelectedModels { get; }

        Task<IEnumerable<TModel>> SelectAsync(IDbConnection dbConnection);
    }
}