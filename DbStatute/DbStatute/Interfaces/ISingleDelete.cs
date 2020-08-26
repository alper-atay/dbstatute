using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleDelete<TSingleSelect> : IDelete
        where TSingleSelect : ISingleSelect
    {
        TSingleSelect SingleSelect { get; }

        Task DeleteAsync(IDbConnection dbConnection);
    }

    public interface ISingleDelete<TModel, TSingleSelect> : IDelete<TModel>, ISingleDelete<TSingleSelect>
        where TModel : class, IModel, new()
        where TSingleSelect : ISingleSelect<TModel>
    {
        new TSingleSelect SingleSelect { get; }

        new Task DeleteAsync(IDbConnection dbConnection);
    }
}