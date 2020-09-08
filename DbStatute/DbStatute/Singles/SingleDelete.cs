using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleDelete<TModel> : SingleDeleteById<TModel>, ISingleDelete<TModel>
        where TModel : class, IModel, new()
    {
        public SingleDelete(TModel sourceModel) : base(sourceModel.Id)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public TModel SourceModel { get; }

        object ISourceModel.SourceModel => SourceModel;

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            return await base.DeleteOperationAsync(dbConnection) ?? SourceModel;
        }
    }
}