/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : This class is intended to parse parameter-names 
 *               to be added to an IDbCommand object.
 *------------------------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Simple.Framework
{
    internal class ParameterParser
    {
        /// <summary>
        /// Parses an SQL query string to get a list of '@parameters'.
        /// </summary>
        /// <param name="queryString">The SQL query string.</param>
        /// <returns>string</returns>
        public static IEnumerable<string> Parse(string queryString)
        {
            Regex regex = new Regex(@"(@[a-z][a-z0-9_]*)", RegexOptions.IgnoreCase);

            foreach (Match m in regex.Matches(queryString))
            {
                yield return m.Value;
            }
        }
    }
}
