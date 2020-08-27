using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Singles
{
    public interface ISingleDeleteBase : IDeleteBase
    {
        object DeletedModel { get; }

        Task<object> DeleteAsync(IDbConnection dbConnection);
    }

    public interface ISingleDeleteBase<TModel> : IDeleteBase<TModel>, ISingleDeleteBase
        where TModel : class, IModel, new()
    {
        new TModel DeletedModel { get; }

        new Task<TModel> DeleteAsync(IDbConnection dbConnection);
    }
}