using MM.Suppliers.API.Web.Mappings;

namespace MM.Suppliers.API.Web.Extenstions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapperConfigurations(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SuppliersProfiles));
        }
    }
}
