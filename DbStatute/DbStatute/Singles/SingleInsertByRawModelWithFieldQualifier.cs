using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using DbStatute.Interfaces.Singles;
using DbStatute.Querying.Qualifiers;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByRawModelWithFieldQualifier<TModel, TFieldQualifier> : SingleInsertBase<TModel>, ISingleInsertByRawModelWithFieldQualifier<TModel, TFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : FieldQualifier<TModel>, new()
    {
        public SingleInsertByRawModelWithFieldQualifier(TModel rawModel)
        {
            RawModel = rawModel;
            FieldQualifier = new FieldQualifier<TModel>() as TFieldQualifier;
        }

        public SingleInsertByRawModelWithFieldQualifier(TModel rawModel, TFieldQualifier fieldQualifier)
        {
            RawModel = rawModel;
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public TFieldQualifier FieldQualifier { get; }
        public TModel RawModel { get; }
        object IRawModel.RawModel => RawModel;

        protected override Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            throw new System.NotImplementedException();
        }
    }
}