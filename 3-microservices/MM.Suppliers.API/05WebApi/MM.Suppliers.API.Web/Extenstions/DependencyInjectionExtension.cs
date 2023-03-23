using AutoMapper;
using MM.Base.Core.Entities;
using MM.Base.Core.Repositories;
using MM.Base.Core.Services;
using MM.Suppliers.API.AzureServices;
using MM.Suppliers.API.Common.Interfaces;
using MM.Suppliers.API.Domain.Entities;
using MM.Suppliers.API.Repositories;
using MM.Suppliers.API.Services;
using MM.Suppliers.API.Web.Filters;

namespace MM.Suppliers.API.Web.Extenstions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependenciesInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAzureService, AzureService>();


            services.AddSingleton<DapperContext<Supplier>>();
            services.AddScoped<ISuppliersRepository>(x => new SuppliersRepository(x.GetService<DapperContext<Supplier>>(), x.GetService<IMapper>()));
            services.AddScoped<ISuppliersService, SuppliersService>();

            services.AddScoped<ValidationFilterAttribute>();

            services.AddSingleton<DapperContext<AuthorizeEntity>>();
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<IAuthorizeRepository>(x => new AuthorizeRepository(x.GetService<DapperContext<AuthorizeEntity>>(), x.GetService<IMapper>()));
        }
    }
}
