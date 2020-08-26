using DbStatute.Interfaces.Querying.Statutes;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleUpdate<TUpdateQuery, TSingleSelect> : IUpdate
        where TUpdateQuery : IUpdateQuery
        where TSingleSelect : ISingleSelect
    {
        TSingleSelect SingleSelect { get; }
        object UpdatedModel { get; }
        TUpdateQuery UpdateQuery { get; }

        Task<object> UpdateAsync(IDbConnection dbConnection);
    }

    public interface ISingleUpdate<TModel, TUpdateQuery, TSingleSelect> : IUpdate<TModel>, ISingleUpdate<TUpdateQuery, TSingleSelect>
        where TModel : class, IModel, new()
        where TUpdateQuery : IUpdateQuery<TModel>
        where TSingleSelect : ISingleSelect<TModel>
    {
        new TSingleSelect SingleSelect { get; }
        new TModel UpdatedModel { get; }
        new TUpdateQuery UpdateQuery { get; }

        new Task<TModel> UpdateAsync(IDbConnection dbConnection);
    }
}