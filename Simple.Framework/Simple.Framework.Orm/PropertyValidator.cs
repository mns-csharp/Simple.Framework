using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework.Orm
{
    public class PropertyValidator
    {
        public static void ValidateNullable(Property prop, Object value)
        {
            bool isNullable = PropertyDataExtractor.IsNullable(prop);
            bool isNullValue = (value==null);

            if (!isNullable)
            {
                if (isNullValue)
                {
                    throw new Exception("Property : " + PropertyDataExtractor.GetName(prop) + ".\nMessage : Value cannot be empty.");
                }
            }            
        }

        public static void ValidateMinMax(Property prop, Object value)
        {
            Type type = PropertyDataExtractor.GetType(prop);

            Object minValue = PropertyDataExtractor.GetMinValue(prop);
            Object maxValue = PropertyDataExtractor.GetMaxValue(prop);
            Object actualVaue = null;

            bool minValueOk = false;
            bool maxValueOk = false;

            if (minValue != null)
            {
                switch (type.Name)
                {
                    case "Boolean":
                        break;

                    case "SByte":
                        actualVaue = Convert.ToSByte(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((sbyte)minValue) <= ((sbyte)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Byte":
                        actualVaue = Convert.ToByte(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((byte)minValue) <= ((byte)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Byte[]":
                        break;

                    case "DateTime":
                        actualVaue = Convert.ToDateTime(value);
                        
                        if (actualVaue != null)
                        {
                            minValueOk = ((DateTime)minValue).Date <= ((DateTime)actualVaue).Date;
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Int16":
                        actualVaue = Convert.ToInt16(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((short)minValue) <= ((Int16)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Int32":
                        actualVaue = Convert.ToInt32(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((int)minValue) <= ((Int32)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Int64":
                        actualVaue = Convert.ToInt64(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((long)minValue) <= ((Int64)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Single":
                        actualVaue = Convert.ToByte(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((float)minValue) <= ((Single)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Double":
                        actualVaue = Convert.ToByte(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((double)minValue) <= ((double)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Decimal":
                        actualVaue = Convert.ToByte(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((decimal)minValue) <= ((decimal)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Char":
                        actualVaue = Convert.ToChar(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((char)minValue) <= ((char)actualVaue);
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "String":
                        actualVaue = Convert.ToString(value);
                        if (actualVaue != null)
                        {
                            minValueOk = ((int)minValue) <= ((string)actualVaue).Length;
                        }
                        else
                        {
                            minValue = true;
                        }
                        break;

                    case "Guid":
                        break;
                }
            }

            if (maxValue != null)
            {
                switch (type.Name)
                {
                    case "Boolean":
                        break;

                    case "SByte":
                        actualVaue = Convert.ToSByte(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((sbyte)minValue) >= ((sbyte)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Byte":
                        actualVaue = Convert.ToByte(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((byte)minValue) >= ((byte)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Byte[]":
                        break;

                    case "DateTime":
                        actualVaue = Convert.ToDateTime(value);
                        if (actualVaue != null)
                        {                            
                            maxValueOk = ((DateTime)maxValue).Date >= ((DateTime)actualVaue).Date;
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Int16":
                        actualVaue = Convert.ToInt16(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((short)minValue) >= ((Int16)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Int32":
                        actualVaue = Convert.ToInt32(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((int)minValue) >= ((Int32)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Int64":
                        actualVaue = Convert.ToInt64(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((long)minValue) >= ((Int64)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Single":
                        actualVaue = Convert.ToByte(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((float)minValue) >= ((Single)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Double":
                        actualVaue = Convert.ToByte(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((double)minValue) >= ((double)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Decimal":
                        actualVaue = Convert.ToByte(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((decimal)minValue) >= ((decimal)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Char":
                        actualVaue = Convert.ToChar(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((char)minValue) >= ((char)actualVaue);
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "String":
                        actualVaue = Convert.ToString(value);
                        if (actualVaue != null)
                        {
                            maxValueOk = ((int)maxValue) >= ((string)actualVaue).Length;
                        }
                        else
                        {
                            maxValue = true;
                        }
                        break;

                    case "Guid":
                        break;
                }
            }

            if (minValue == null)
            {
                minValueOk = true;
            }

            if (maxValue == null)
            {
                maxValueOk = true;
            }

            if (!minValueOk && !maxValueOk)
            {
                throw new Exception("Property : " + PropertyDataExtractor.GetName(prop) +".\nMessage : "+ PropertyDataExtractor.GetMinValueErrorMessage(prop) + " " + PropertyDataExtractor.GetMaxValueErrorMessage(prop));
            }
            else if (!minValueOk)
            {
                throw new Exception("Property : " + PropertyDataExtractor.GetName(prop) + ".\nMessage : " + PropertyDataExtractor.GetMinValueErrorMessage(prop));
            }
            else if (!maxValueOk)
            {
                throw new Exception("Property : " + PropertyDataExtractor.GetName(prop) + ".\nMessage : " + PropertyDataExtractor.GetMaxValueErrorMessage(prop));
            }
        }
    }
}

