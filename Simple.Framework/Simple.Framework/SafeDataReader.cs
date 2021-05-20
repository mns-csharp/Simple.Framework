using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Simple.Framework
{
    public class SafeDataReader : ASafeDataReader, ISafeDataReader
    {
        public SafeDataReader(IDataReader reader):base(reader)
        {
        }
    }
}
