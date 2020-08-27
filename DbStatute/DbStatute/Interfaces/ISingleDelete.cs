using DbStatute.Interfaces.Fundamentals;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleDelete<TSingleSelect> : IDeleteBase
        where TSingleSelect : ISingleSelect
    {
        TSingleSelect SingleSelect { get; }

        Task DeleteAsync(IDbConnection dbConnection);
    }

    public interface ISingleDelete<TModel, TSingleSelect> : IDeleteBase<TModel>, ISingleDelete<TSingleSelect>
        where TModel : class, IModel, new()
        where TSingleSelect : ISingleSelect<TModel>
    {
        new TSingleSelect SingleSelect { get; }

        new Task DeleteAsync(IDbConnection dbConnection);
    }
}