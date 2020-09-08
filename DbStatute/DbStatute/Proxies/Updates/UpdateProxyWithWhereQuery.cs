using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces.Proxies.Updates;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbStatute.Proxies.Updates
{
    public class UpdateProxyWithWhereQuery : UpdateProxyBase, IUpdateProxyWithWhereQuery
    {
        public UpdateProxyWithWhereQuery()
        {
            WhereQuery = new WhereQuery();
        }

        public UpdateProxyWithWhereQuery(WhereQuery whereQuery)
        {
            WhereQuery = whereQuery ?? throw new ArgumentNullException(nameof(whereQuery));
        }

        public UpdateProxyWithWhereQuery(WhereQuery whereQuery, IFieldQualifier updatedFieldQualifier) : base(updatedFieldQualifier)
        {
            WhereQuery = whereQuery ?? throw new ArgumentNullException(nameof(whereQuery));
        }

        public IWhereQuery WhereQuery { get; }
    }
}
