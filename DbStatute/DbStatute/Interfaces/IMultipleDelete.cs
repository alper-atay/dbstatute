using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{


    public interface IMultipleDelete<TModel, TMultipleSelect> : IDelete<TModel>
        where TModel : class, IModel, new()
        where TMultipleSelect : IMultipleSelect<TModel>
    {
        TMultipleSelect MultipleSelect { get; }

        IAsyncEnumerable<TModel> DeleteAsSinglyAsync(IDbConnection dbConnection);

        Task<int> DeleteAsync(IDbConnection dbConnection);

        Task<int> DeleteByActingAsync(IDbConnection dbConnection, Action<TModel> action);
    }
}