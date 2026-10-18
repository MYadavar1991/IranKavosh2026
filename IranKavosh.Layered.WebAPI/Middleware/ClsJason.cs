using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IranKavosh.Layered.WebAPI.Middleware
{
    public static class ClsJason
    {
        //public class DateTimeJsonConvert : JsonConverter<DateTime>
        //{
        //    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        //    {
        //        Debug.Assert(typeToConvert == typeof(DateTime));
        //        return DateTime.Parse(reader.GetString());
        //    }

        //    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        //    {
        //        writer.WriteStringValue(value.ToString());
        //    }
        //}
        //public static JsonSerializerOptions JsonSerializerOptions()
        //{
        //    JsonSerializerOptions options = new JsonSerializerOptions();
        //    options.Converters.Add(new DateTimeJsonConvert());
        //    return options;
        //}
        public static IsoDateTimeConverter IsoDateTimeConverter()
        {
            var format = "yyyy/MM/dd HH:mm:ss";
            IsoDateTimeConverter dateTimeConverter;
            //dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format,Culture = new CultureInfo("en-US") };
            dateTimeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = format,
                Culture = CultureInfo.InvariantCulture,
                
            };

            return dateTimeConverter;
        }
        public static JsonSerializerSettings IsoDateTimeConverter2()
        {
            var format = "yyyy/MM/dd HH:mm:ss";
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = format,
                Culture = CultureInfo.InvariantCulture,
            };
      

            return settings;
        }

    }
}
