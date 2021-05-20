using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework.Orm
{
    public class SQLGenerator
    {
        private Mapping xmlMapping;

        public SQLGenerator(Mapping hbm)
        {
            xmlMapping = hbm;
        }

        #region Get
        private KeyValuePair<string, string> GetQuery;
        public KeyValuePair<string, string> GetAll()
        {
            if (!string.IsNullOrEmpty(GetQuery.Key))
            {
                return GetQuery;
            }
            else
            {
                StringBuilder sb = new StringBuilder("SELECT ");

                if (xmlMapping.Class.Id != null)
                {
                    sb.Append(xmlMapping.Class.Id.ColumnName + ", ");
                }

                foreach (Property p in xmlMapping.Class.Properties)
                {
                    sb.Append(PropertyDataExtractor.GetColumnName(p) + ", ");
                }
                sb = sb.Remove(sb.Length - 2, 2);
                sb.Append(" FROM ");
                sb.Append(xmlMapping.Class.TableName);

                GetQuery = new KeyValuePair<string, string>(KeyMaker.GetKey(xmlMapping, QueryNameConst.GetAll), sb.ToString());

                return GetQuery;
            }
        }
        #endregion

        #region GetByID
        private KeyValuePair<string, string> GetByIDQuery;
        public KeyValuePair<string, string> GetByID()
        {
            if (!string.IsNullOrEmpty(GetByIDQuery.Key))
            {
                return GetByIDQuery;
            }
            else
            {
                if (xmlMapping.Class.Id != null)
                {
                    StringBuilder sb = new StringBuilder(this.GetAll().Value);
                    sb.Append(" WHERE " + xmlMapping.Class.Id.ColumnName);
                    sb.Append("=@" + xmlMapping.Class.Id.ColumnName);


                    GetByIDQuery = new KeyValuePair<string, string>(KeyMaker.GetKey(xmlMapping, QueryNameConst.GetByID), sb.ToString());
                }
                else
                {
                    GetByIDQuery = new KeyValuePair<string, string>(string.Empty, string.Empty);
                }

                return GetByIDQuery;
            }
        }
        #endregion

        #region GetByFieldValue
        public KeyValuePair<string, string> GetByFieldValue(string fieldName)
        {
            KeyValuePair<string, string> selectQuery;

            StringBuilder sb = new StringBuilder(this.GetAll().Value);
            sb.Append(" WHERE " + fieldName);
            sb.Append("=@" + fieldName);

            selectQuery = new KeyValuePair<string, string>(KeyMaker.GetKey(xmlMapping, QueryNameConst.GetBy + fieldName), sb.ToString());

            return selectQuery;
        }
        #endregion

        #region InsertByID
        private KeyValuePair<string, string> InsertByIDQuery;
        public KeyValuePair<string, string> InsertByID()
        {
            if (!string.IsNullOrEmpty(InsertByIDQuery.Key))
            {
                return InsertByIDQuery;
            }
            else
            {
                StringBuilder sb = new StringBuilder("INSERT INTO " + xmlMapping.Class.TableName + "(");

                if (xmlMapping.Class.Id != null)
                {
                    sb.Append(xmlMapping.Class.Id.ColumnName + ", ");
                }

                foreach (Property p in xmlMapping.Class.Properties)
                {
                    sb.Append(PropertyDataExtractor.GetColumnName(p) + ", ");
                }
                sb = sb.Remove(sb.Length - 2, 2);
                sb.Append(") VALUES(");

                if (xmlMapping.Class.Id != null)
                {
                    sb.Append("@" + xmlMapping.Class.Id.ColumnName + ", ");
                }

                foreach (Property p in xmlMapping.Class.Properties)
                {
                    sb.Append("@" + PropertyDataExtractor.GetColumnName(p) + ", ");
                }
                sb = sb.Remove(sb.Length - 2, 2);
                sb.Append(")");

                InsertByIDQuery = new KeyValuePair<string, string>(KeyMaker.GetKey(xmlMapping, QueryNameConst.Save), sb.ToString());

                return InsertByIDQuery;
            }
        }
        #endregion

        #region UpdateByID
        private KeyValuePair<string, string> UpdateByIdQuery;
        public KeyValuePair<string, string> UpdateByID()
        {
            if (!string.IsNullOrEmpty(UpdateByIdQuery.Key))
            {
                return UpdateByIdQuery;
            }
            else
            {
                StringBuilder sb = new StringBuilder("UPDATE " + xmlMapping.Class.TableName + " SET ");

                foreach (Property p in xmlMapping.Class.Properties)
                {
                    sb.Append(PropertyDataExtractor.GetColumnName(p) + "=@" + PropertyDataExtractor.GetColumnName(p) + ", ");
                }
                sb = sb.Remove(sb.Length - 2, 2);

                if (xmlMapping.Class.Id != null)
                {
                    sb.Append(" WHERE ");
                    sb.Append(xmlMapping.Class.Id.ColumnName + "=@" + xmlMapping.Class.Id.ColumnName);
                }

                UpdateByIdQuery = new KeyValuePair<string, string>(KeyMaker.GetKey(xmlMapping, QueryNameConst.UpdateByID), sb.ToString());

                return UpdateByIdQuery;
            }
        }
        #endregion

        #region UpdateByFieldValue
        public KeyValuePair<string, string> UpdateByFieldValue(string fieldName)
        {
            KeyValuePair<string, string> updateQuery;

            StringBuilder sb = new StringBuilder("UPDATE " + xmlMapping.Class.TableName + " SET ");

            foreach (Property p in xmlMapping.Class.Properties)
            {
                sb.Append(PropertyDataExtractor.GetColumnName(p) + "=@" + PropertyDataExtractor.GetColumnName(p) + ", ");
            }
            sb = sb.Remove(sb.Length - 2, 2);
            sb.Append(" WHERE ");
            sb.Append(fieldName + "=@" + fieldName);

            updateQuery = new KeyValuePair<string, string>(KeyMaker.GetKey(xmlMapping, QueryNameConst.UpdateBy + fieldName), sb.ToString());

            return updateQuery;
        }
        #endregion

        #region DeleteByID
        private KeyValuePair<string, string> DeleteByIDQuery;
        public KeyValuePair<string, string> DeleteByID()
        {
            if (!string.IsNullOrEmpty(DeleteByIDQuery.Key))
            {
                return DeleteByIDQuery;
            }
            else
            {

                StringBuilder sb = new StringBuilder("DELETE FROM " + xmlMapping.Class.TableName);

                if (xmlMapping.Class.Id != null)
                {
                    sb.Append(" WHERE ");
                    sb.Append(xmlMapping.Class.Id.ColumnName + "=@" + xmlMapping.Class.Id.ColumnName);
                }

                DeleteByIDQuery = new KeyValuePair<string, string>(KeyMaker.GetKey(xmlMapping, QueryNameConst.DeleteByID), sb.ToString());

                return DeleteByIDQuery;
            }
        }
        #endregion

        #region DeleteByFieldValue
        public KeyValuePair<string, string> DeleteByFieldValue(string fieldName)
        {
            KeyValuePair<string, string> deleteQuery;

            StringBuilder sb = new StringBuilder("DELETE FROM " + xmlMapping.Class.TableName + " WHERE ");

            sb.Append(fieldName + "=@" + fieldName);

            deleteQuery = new KeyValuePair<string, string>(KeyMaker.GetKey(xmlMapping, QueryNameConst.DeleteBy, fieldName), sb.ToString());

            return deleteQuery;
        }
        #endregion
    }
}
