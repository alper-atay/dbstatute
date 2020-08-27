using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Singles
{
    public interface ISingleUpdateBase : IUpdateBase
    {
        object UpdatedModel { get; }

        Task<object> UpdateAsync(IDbConnection dbConnection);
    }

    public interface ISingleUpdateBase<TModel> : IUpdateBase<TModel>, ISingleUpdateBase
        where TModel : class, IModel, new()
    {
        new TModel UpdatedModel { get; }

        new Task<TModel> UpdateAsync(IDbConnection dbConnection);
    }
}