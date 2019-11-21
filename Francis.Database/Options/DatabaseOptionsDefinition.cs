using System;

namespace Francis.Database.Options
{
    public class DatabaseOptionsDefinition
    {
        public Type Type { get; set; }

        public string Name => Type.Name.Replace("Options", "");


        public DatabaseOptionsDefinition(Type type)
        {
            Type = type;
        }
    }

    public class DatabaseOptionsDefinition<TOptions> : DatabaseOptionsDefinition
        where TOptions : class, new()
    {
        public DatabaseOptionsDefinition() : base(typeof(TOptions)) { }
    }
}
