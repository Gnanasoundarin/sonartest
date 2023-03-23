namespace MM.Suppliers.API.Web.Extenstions
{
    public static class LocalizationExtension
    {
        public static void UseCustomRequestLocalization(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(GetLocalizationOptions());
        }
        private static RequestLocalizationOptions GetLocalizationOptions()
        {
            string[] supportedCultures = new[] { "en" };
            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            return localizationOptions;
        }
    }
}
