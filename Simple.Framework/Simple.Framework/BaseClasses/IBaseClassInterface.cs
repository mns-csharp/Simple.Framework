/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : Every Value-Object classes in the VO layer must implement
 *               this interface.
 *------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    /// <summary>
    /// Every Value-Object classes in the VO layer must implement this interface.
    /// </summary>
    public interface IBaseClassInterface
    {
        int? ID { get; set; }
    }
}
