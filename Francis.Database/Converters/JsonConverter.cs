using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Francis.Database.Converters
{
    public static class JsonConverter
    {
        public static PropertyBuilder<TEntity> HasJsonConversion<TEntity>(this PropertyBuilder<TEntity> propertyBuilder)
        {
            ValueConverter<TEntity, string> converter = new ValueConverter<TEntity, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<TEntity>(v));

            ValueComparer<TEntity> comparer = new ValueComparer<TEntity>(
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<TEntity>(JsonConvert.SerializeObject(v)));

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);

            return propertyBuilder;
        }
    }
}
