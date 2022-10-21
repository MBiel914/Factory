namespace Factory.API.Service.Configurations
{
    public interface IMapable<TSource, TDestination>
        where TDestination : class
        where TSource : class
    {
        public TDestination Map(TSource source);
    }
}
