/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : This class is intended to create instances of various database-independent
 *               database objects that are used in the Framework or in DAL layer.
 *               
 *               We must supply a Provider-Name to the constructor.
 *------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace Simple.Framework
{
    public sealed class DbObjectInstantiator
    {
        public static IDbConnection GetConnectionInstance(string providerName)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerName);
            return providerFactory.CreateConnection();
        }

        public static IDbCommand GetCommandInstance(string providerName)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerName);
            return providerFactory.CreateCommand();
        }

        public static IDataParameter GetParameterInstance(string providerName)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerName);
            return providerFactory.CreateParameter();
        }

        public static IDbDataAdapter GetDataAdapterInstance(string providerName)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerName);
            return providerFactory.CreateDataAdapter();
        }
    }
}
