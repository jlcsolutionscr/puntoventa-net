using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace LeandroSoftware.Core.CustomClasses
{
    public class CustomJavascriptSerializer : JavaScriptSerializer
    {
        public CustomJavascriptSerializer()
        : base()
        {
            RegisterConverters(new JavaScriptConverter[] { new DateStringJSONConverter() });
        }
    }

    public class DateStringJSONConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new List<Type>() { typeof(DateTime), typeof(DateTime?) }; }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (obj == null) return result;
            result["DateTime"] = ((DateTime)obj).ToString("dd-MM-yyyy HH:mm:ss \"GMT\"zzz");
            return result;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary.ContainsKey("DateTime"))
            {
                return DateTime.ParseExact(dictionary["DateTime"].ToString(), "dd-MM-yyyy HH:mm:ss \"GMT\"zzz", System.Globalization.CultureInfo.CurrentCulture);
            }
            return null;
        }
    }
}
