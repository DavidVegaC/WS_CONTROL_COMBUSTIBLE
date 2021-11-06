using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;

namespace AccesoDatos
{
    public class DataUtil
    {
        public static object GetReaderValue(IDataReader reader, string columnName)
        {
            DataTable schema = reader.GetSchemaTable();
            DataRow[] rows = schema.Select(string.Format("ColumnName='{0}'", columnName));
            if ((rows != null) && (rows.Length > 0))
            {
                return reader[columnName];
            }
            else return null;
        }

        public static Int64 ObjectToInt64(IDataReader reader, string columnName)
        {
            return ObjectToInt64(GetReaderValue(reader, columnName));
        }
        public static Int64 ObjectToInt64(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToInt64(obj);
        }

        public static Int16 ObjectToInt16(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? Int16.MinValue : Convert.ToInt16(obj);
        }
        public static Int16 ObjectToInt16(IDataReader reader, string columnName)
        {
            return ObjectToInt16(GetReaderValue(reader, columnName));
        }

        public static Int32 ObjectToInt32(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToInt32(obj);
        }
        public static Int32? ObjectToInt32Null(object obj)
        {
            Int32? valor = null;
            if ((obj == null) || (obj == DBNull.Value))
            { return valor; }
            else
            { valor = Convert.ToInt32(obj); }
            return valor;
        }
        public static Int32 ObjectToInt32(IDataReader reader, string columnName)
        {
            return ObjectToInt32(GetReaderValue(reader, columnName));
        }


        public static decimal ObjectToDecimal(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? 0.00M : Convert.ToDecimal(obj);
        }
        public static decimal ObjectToDecimal(IDataReader reader, string columnName)
        {
            return ObjectToDecimal(GetReaderValue(reader, columnName));
        }

        public static decimal StrToDecimal(string obj)
        {
            return ((obj == null) || (obj == "")) ? 0.00M : Convert.ToDecimal(obj);
        }
        public static decimal StrToDecimal(IDataReader reader, string columnName)
        {
            return StrToDecimal(ObjectToString(GetReaderValue(reader, columnName)));
        }

        public static int ObjectToInt(object obj)
        {
            return ObjectToInt32(obj);
        }
        public static int ObjectToInt(IDataReader reader, string columnName)
        {
            return ObjectToInt(GetReaderValue(reader, columnName));
        }


        public static double ObjectToDouble(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToDouble(obj);
        }
        public static double ObjectToDouble(IDataReader reader, string columnName)
        {
            return ObjectToDouble(GetReaderValue(reader, columnName));
        }


        public static bool ObjectToBool(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? false : Convert.ToBoolean(obj);
        }
        public static bool ObjectToBool(IDataReader reader, string columnName)
        {
            return ObjectToBool(GetReaderValue(reader, columnName));
        }


        public static string ObjectToString(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? "" : Convert.ToString(obj);
        }
        public static string ObjectToString(IDataReader reader, string columnName)
        {
            return ObjectToString(GetReaderValue(reader, columnName));
        }

        public static DateTime ObjectToDateTime(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? DateTime.MinValue : Convert.ToDateTime(obj);
        }
        public static DateTime? ObjectToDateTimeNull(object obj)
        {
            DateTime? valor = null;
            if ((obj == null) || (obj == DBNull.Value))
            { return valor; }
            else
            { valor = Convert.ToDateTime(obj); }
            return valor;
        }
        public static DateTime ObjectToDateTime(IDataReader reader, string columnName)
        {
            return ObjectToDateTime(GetReaderValue(reader, columnName));
        }



        public static DateTime StringToDateTime(string str)
        {
            return ((str == "")) ? DateTime.MinValue : Convert.ToDateTime(str);
        }
        public static DateTime StringToDateTime(IDataReader reader, string columnName)
        {
            return StringToDateTime(ObjectToString(GetReaderValue(reader, columnName)));
        }


        public static byte ObjectToByte(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? byte.MinValue : Convert.ToByte(obj);
        }
        public static byte ObjectToByte(IDataReader reader, string columnName)
        {
            return ObjectToByte(GetReaderValue(reader, columnName));
        }


        public static int StringToInt(string str)
        {
            return ((str == null) || (str == "")) ? 0 : Convert.ToInt32(str);
        }
        public static int StringToInt(IDataReader reader, string columnName)
        {
            return StringToInt(ObjectToString(GetReaderValue(reader, columnName)));
        }


        public static object IntToDBNull(int int1)
        {
            return ((int1 == 0)) ? DBNull.Value : (object)int1;
        }
        public static object IntToDBNull(IDataReader reader, string columnName)
        {
            return IntToDBNull(ObjectToInt(GetReaderValue(reader, columnName)));
        }

        public static object Int32ToDBNull(Int32 int1)
        {
            return ((int1 == 0)) ? DBNull.Value : (object)int1;
        }
        public static object Int32ToDBNull(IDataReader reader, string columnName)
        {
            return Int32ToDBNull(ObjectToInt32(GetReaderValue(reader, columnName)));
        }


        public static object Int64ToDBNull(Int64 int1)
        {
            return ((int1 == 0)) ? DBNull.Value : (object)int1;
        }
        public static object Int64ToDBNull(IDataReader reader, string columnName)
        {
            return Int64ToDBNull(ObjectToInt64(GetReaderValue(reader, columnName)));
        }


        public static object DateTimeToDBNull(DateTime date)
        {
            return ((date == DateTime.MinValue)) ? DBNull.Value : (object)date;
        }
        public static object DateTimeToDBNull(IDataReader reader, string columnName)
        {
            return DateTimeToDBNull(ObjectToDateTime(GetReaderValue(reader, columnName)));
        }

        public static string ObjectDecimalToStringFormatMiles(object obj)
        {
            return ObjectToDecimal(obj).ToString("#,#0.00");
        }
        public static string ObjectDecimalToStringFormatMiles(IDataReader reader, string columnName)
        {
            return ObjectDecimalToStringFormatMiles(GetReaderValue(reader, columnName));
        }


        public static string BoolToString(bool flag)
        {
            return flag ? "1" : "0";
        }
        public static string BoolToString(IDataReader reader, string columnName)
        {
            return BoolToString(ObjectToBool(GetReaderValue(reader, columnName)));
        }


        public static bool StringToBool(string flag)
        {
            return flag.Equals("1");
        }
        public static bool StringToBool(IDataReader reader, string columnName)
        {
            return StringToBool(ObjectToString(GetReaderValue(reader, columnName)));
        }


        public static string IntToString(int int1)
        {
            return ((int1 == 0)) ? "" : Convert.ToString(int1);
        }
        public static string IntToString(IDataReader reader, string columnName)
        {
            return IntToString(ObjectToInt(GetReaderValue(reader, columnName)));
        }        
    }
}
