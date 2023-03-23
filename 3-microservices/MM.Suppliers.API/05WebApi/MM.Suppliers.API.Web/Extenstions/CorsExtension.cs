namespace MM.Suppliers.API.Web.Extenstions
{
    public static class CorsExtension
    {
        public static void AddCorsConfigurations(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                      builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                                  });
            });
        }
    }
}
