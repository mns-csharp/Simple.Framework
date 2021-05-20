/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : This class is intended to create and store some TransactionManager
 *               objects that are automatically created by using connection strings
 *               supplied in tha App.config file.
 *               
 *               We must supply ProviderName along with each of the connection strings.
 *               A TransactionManager can not be created without ProviderName.
 *------------------------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;

namespace Simple.Framework
{
    public static class ApplicationContext
    {
        /// <summary>
        /// This property will hold all the instances of TransactionContexts used by our application.
        /// </summary>
        private static ConnectionStringSettingsContainer container;

        /// <summary>
        /// Static constructor.
        /// 
        /// This constructor is responsible for reading connection-strings and
        /// creating one TransactionManager for each of them.
        /// </summary>
        static ApplicationContext()
        {
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                if (container == null)
                {
                    container = new ConnectionStringSettingsContainer();
                }

                container.Add(new KeyValuePair<string, ConnectionStringSettings>(css.Name, css));
            }
        }

        public static ConnectionStringSettings Get(string key)
        {
            ConnectionStringSettings css = container.Get(key);

            return css;
        }
    }
}

