using  Microsoft.Extensions.DependencyInjection;

namespace Static.Service
{
    public static class ServiceHandler 
    {
        public static IServiceProvider? provider {get;set;}

        public static Type GetService<Type>()
        {
            if (provider is not null)
            {
                using (var scope = provider.CreateScope())
                {
                    var _type = scope.ServiceProvider.GetRequiredService<Type>();
                    return _type;
                }
            }
            else
            {
                throw new Exception("service provider is  null");
            }

        }

    }
}

 