/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : This class is intended to access data 
 *               through a data reader in a safe way,
 *               by handling the null values from the 
 *               database-tables.
 *------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;

namespace Simple.Framework
{
    public abstract class ASafeDataReader : ISafeDataReader, IDisposable
    {
        private IDataReader reader = null;

        #region public SafeDataReader(IDataReader reader)
        public ASafeDataReader(IDataReader reader)
        {
            this.reader = reader;
        } 
        #endregion

        #region bool? GetBoolean(String column)
        public virtual bool? GetBoolean(String column)
        {
            bool? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToBoolean(reader[column]);
            }

            return data;
        } 
        #endregion

        #region sbyte? GetSByte(String column)
        public virtual sbyte? GetSByte(String column)
        {
            sbyte? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToSByte(reader[column]);
            }

            return data;
        } 
        #endregion

        #region byte? GetByte(String column)
        public virtual byte? GetByte(String column)
        {
            byte? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToByte(reader[column]);
            }

            return data;
        } 
        #endregion

        #region byte [] GetByteArray(String column)
        public virtual byte[] GetByteArray(String column)
        {
            byte[] data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = (byte[])reader[column];
            }

            return data;
        } 
        #endregion

        #region DateTime? GetDateTime(String column)
        public virtual DateTime? GetDateTime(String column)
        {
            DateTime? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToDateTime(reader[column]);
            }

            return data;
        }  
        #endregion       
        
        #region short? GetInt16(String column)
        public virtual short? GetInt16(String column)
        {
            short? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToInt16(reader[column]);
            }

            return data;
        } 
        #endregion

        #region int? GetInt32(String column)
        public virtual int? GetInt32(String column)
        {
            int? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToInt32(reader[column]);
            }

            return data;
        } 
        #endregion

        #region long? GetInt64(String column)
        public virtual long? GetInt64(String column)
        {
            long? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToInt64(reader[column]);
            }

            return data;
        } 
        #endregion

        #region float? GetFloat(String column)
        public virtual float? GetFloat(String column)
        {
            float? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToSingle(reader[column]);
            }

            return data;
        } 
        #endregion

        #region double? GetDouble(String column)
        public virtual double? GetDouble(String column)
        {
            double? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToDouble(reader[column]);
            }

            return data;
        } 
        #endregion

        #region decimal? GetDecimal(String column)
        public virtual decimal? GetDecimal(String column)
        {
            decimal? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToDecimal(reader[column]);
            }

            return data;
        } 
        #endregion

        #region char? GetChar(String column)
        public virtual char? GetChar(String column)
        {
            char? data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = Convert.ToChar(reader[column]);
            }

            return data;
        } 
        #endregion

        #region String GetString(String column)
        public virtual String GetString(String column)
        {
            String data = string.Empty;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = reader[column].ToString();
            }

            return data;
        } 
        #endregion

        #region Image GetImage(String column)
        public virtual Image GetImage(String column)
        {
            Image image = null;

            byte[] buffer = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                buffer = this.GetByteArray(column);

                image = ConvertImage.ToImage(buffer);
            }

            return image;
        } 
        #endregion

        #region Object GetObject(String column)
        public virtual Object GetObject(String column)
        {
            Object data = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column)))
            {
                data = (Object)reader[column];
            }

            return data;
        } 
        #endregion

        #region Guid GetGuid(String column)
        public virtual Guid GetGuid(String column)
        {
            return reader.IsDBNull(reader.GetOrdinal(column)) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal(column));
        } 
        #endregion

        #region bool Read()
        public virtual bool Read()
        {
            return this.reader.Read();
        } 
        #endregion

        #region void Close()
        public virtual void Close()
        {
            if (reader != null)
            {
                if (reader.IsClosed == false)
                {
                    reader.Close();
                }
            }
        } 
        #endregion

        #region void Dispose()
        public virtual void Dispose()
        {
            this.Close();
        } 
        #endregion
    }
}
