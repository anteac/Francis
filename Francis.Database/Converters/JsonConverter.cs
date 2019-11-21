using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Francis.Database.Converters
{
    public static class JsonConverter
    {
        public static PropertyBuilder<TEntity> HasJsonConversion<TEntity>(this PropertyBuilder<TEntity> builder)
        {
            builder.HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<TEntity>(x)
            );

            return builder;
        }
    }
}
