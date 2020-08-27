using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Singles
{
    public interface ISingleSelectBase : ISelectBase
    {
        object SelectedModel { get; }

        Task<object> SelectAsync(IDbConnection dbConnection);
    }

    public interface ISingleSelectBase<TModel> : ISelectBase<TModel>, ISingleSelectBase
        where TModel : class, IModel, new()
    {
        new TModel SelectedModel { get; }

        new Task<TModel> SelectAsync(IDbConnection dbConnection);
    }
}
