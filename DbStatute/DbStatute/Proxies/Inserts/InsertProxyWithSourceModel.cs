using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies.Inserts;
using DbStatute.Interfaces.Qualifiers;
using System;

namespace DbStatute.Proxies.Inserts
{
    public class InsertProxyWithSourceModel : InsertProxy, IInsertProxyWithSourceModel
    {
        public InsertProxyWithSourceModel(object sourceModel)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public InsertProxyWithSourceModel(object sourceModel, IFieldQualifier insertedFieldQualifier) : base(insertedFieldQualifier)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public object SourceModel { get; }
    }

    public class InsertProxyWithSourceModel<TModel> : InsertProxy<TModel>, IInsertProxyWithSourceModel<TModel>
        where TModel : class, IModel, new()
    {
        public InsertProxyWithSourceModel(TModel sourceModel)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public InsertProxyWithSourceModel(TModel sourceModel, IFieldQualifier<TModel> ınsertedFieldQualifier) : base(ınsertedFieldQualifier)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public TModel SourceModel { get; }

        object ISourceModel.SourceModel => SourceModel;
    }
}