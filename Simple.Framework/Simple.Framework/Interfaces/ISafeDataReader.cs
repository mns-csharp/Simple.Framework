using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Simple.Framework
{
    public interface ISafeDataReader : IDisposable
    {
        bool? GetBoolean(String column);
        sbyte? GetSByte(String column);
        byte? GetByte(String column);
        byte[] GetByteArray(String column);
        DateTime? GetDateTime(String column);
        short? GetInt16(String column);
        int? GetInt32(String column);
        long? GetInt64(String column);
        float? GetFloat(String column);
        double? GetDouble(String column);
        decimal? GetDecimal(String column);
        char? GetChar(String column);
        String GetString(String column);
        Image GetImage(String column);
        Object GetObject(String column);
        Guid GetGuid(String column);
        bool Read();
        void Close();
        void Dispose();
    }
}
