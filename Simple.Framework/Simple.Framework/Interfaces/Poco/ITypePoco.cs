using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    public interface ITypePoco<T> : IBaseClassInterface where T : IBaseClassInterface
    {
        string Type { get; set; }
    }
}
