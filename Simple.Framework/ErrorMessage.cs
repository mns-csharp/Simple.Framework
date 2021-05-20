using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework.Orm
{
    public delegate void ItemStateChanged();
    public enum FormViewMode 
    {
        Nothing,
        Add,
        Edit,
        Collection,
        Picker,
        Append
    }
    public class ErrorMessages
    {
        public const string WasNotFound = " was not found!";
    }
}
