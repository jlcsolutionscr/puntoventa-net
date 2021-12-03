using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Globalization;

namespace LeandroSoftware.Core.CustomClasses
{
    public class CustomJavascriptSerializer : JavaScriptSerializer
    {
        public CustomJavascriptSerializer(CultureInfo cultureInfo)
        : base()
        {
            RegisterConverters(new JavaScriptConverter[] { new DateStringJSONConverter(cultureInfo) });
        }
    }

    public class DateStringJSONConverter : JavaScriptConverter
    {
        private CultureInfo _culture;
        private const string _dateFormat = "dd/MM/yyyy HH:mm:ss \"GMT\"zzz";

        public DateStringJSONConverter(CultureInfo cultureInfo)
        : base()
        {
            _culture = cultureInfo;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new List<Type>() { typeof(DateTime), typeof(DateTime?) }; }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (obj == null) return result;
            result["DateTime"] = ((DateTime)obj).ToString(_dateFormat);
            return result;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary.ContainsKey("DateTime"))
            {
                return DateTime.ParseExact(dictionary["DateTime"].ToString(), _dateFormat, _culture);
            }
            return null;
        }
    }
}
