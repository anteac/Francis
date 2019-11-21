using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Francis.Toolbox.JsonConverters
{
    public class SpacedEnumConverter : StringEnumConverter
    {
        public override bool CanConvert(Type objectType) => GetRealType(objectType).IsEnum;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                return Enum.Parse(GetRealType(objectType), reader.Value.ToString().Replace(" ", ""), true);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.WriteJson(writer, value, serializer);
        }


        private Type GetRealType(Type objectType) => Nullable.GetUnderlyingType(objectType) ?? objectType;
    }
}
