namespace Factory.API.Core.Repositories
{
    public static class Mapper
    {
        public static TDestination GenericMapper<TSource, TDestination>(this TSource source)
            where TDestination : new()
        {
            var result = new TDestination();

            var resultProperties = typeof(TDestination).GetProperties()
                .Where(property => property.CanWrite);

            var sourceProperties = typeof(TSource).GetProperties()
                .Where(property => property.CanRead)
                .ToDictionary(property => property.Name);

            foreach (var resultProperty in resultProperties)
            {
                if (sourceProperties.TryGetValue(resultProperty.Name, out var sourceProperty))
                {
                    var sourcePropertyValue = sourceProperty.GetValue(source);
                    if (sourceProperty.PropertyType != resultProperty.PropertyType)
                    {
                        try
                        {
                            //sourcePropertyValue = sourcePropertyValue
                        }
                        catch
                        {
                            sourcePropertyValue = Convert.ChangeType(sourcePropertyValue, resultProperty.PropertyType);
                        }
                    }
                    resultProperty.SetValue(result, sourcePropertyValue);
                }
            }

            return result;
        }
    }
}
