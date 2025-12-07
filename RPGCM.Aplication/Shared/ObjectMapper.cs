namespace RPGCM.Aplication.Shared
{
    public static class ObjectMapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            var destination = new TDestination();
            var sourceProps = typeof(TSource).GetProperties();
            var destProps = typeof(TDestination).GetProperties();

            foreach(var srcProp in sourceProps)
            {
                var destProp = destProps.FirstOrDefault(p => p.Name == srcProp.Name && p.PropertyType == srcProp.PropertyType);
                if(destProp != null && destProp.CanWrite)
                    destProp.SetValue(destination, srcProp.GetValue(source));
            }

            return destination;
        }
    }
}
