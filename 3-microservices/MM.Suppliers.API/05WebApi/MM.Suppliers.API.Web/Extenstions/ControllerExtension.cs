using MM.Suppliers.API.Web.Filters;

namespace MM.Suppliers.API.Web.Extenstions
{
    public static class ControllerExtension
    {
        public static void AddControllerConfigurations(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ValidationFilterAttribute>();
                    options.Filters.Add<SanitizeInputAttribute>();
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                })
     .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);
        }
    }
}
