using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Singles
{
    public interface ISingleInsertBase : IInsertBase
    {
        object InsertedModel { get; }

        Task<object> InsertAsync(IDbConnection dbConnection);
    }

    public interface ISingleInsertBase<TModel> : IInsertBase<TModel>, ISingleInsertBase
        where TModel : class, IModel, new()
    {
        new TModel InsertedModel { get; }

        new Task<TModel> InsertAsync(IDbConnection dbConnection);
    }
}