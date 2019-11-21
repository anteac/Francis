using Newtonsoft.Json;

namespace Francis.Toolbox.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson<T>(this T value) => JsonConvert.SerializeObject(value);

        public static T FromJson<T>(this string value) => JsonConvert.DeserializeObject<T>(value);
    }
}
