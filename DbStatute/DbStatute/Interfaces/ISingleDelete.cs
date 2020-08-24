using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleDelete<TId, TModel, TSingleSelect> : IDelete
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSingleSelect : ISingleSelect<TId, TModel>
    {
        TSingleSelect SingleSelect { get; }

        Task DeleteAsync(IDbConnection dbConnection);
    }
}