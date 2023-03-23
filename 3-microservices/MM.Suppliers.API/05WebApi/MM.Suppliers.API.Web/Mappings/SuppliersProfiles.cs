using AutoMapper;
using MM.Base.Core.Entities;
using MM.Base.Core.Models;
using MM.Suppliers.API.Common.Models;
using MM.Suppliers.API.Domain.Entities;
using MM.Suppliers.API.Web.APIModels;

namespace MM.Suppliers.API.Web.Mappings
{
    public class SuppliersProfiles : Profile
    {
        public SuppliersProfiles()
        {
            CreateMap<SuppliersCreateRequestModel, SuppliersModel>();
            CreateMap<SuppliersModel, Supplier>();
            CreateMap<Supplier, SuppliersModel>();
            CreateMap<SuppliersUpdateRequestModel, SuppliersModel>();
            CreateMap<SuppliersApiPagingRequestModel, SuppliersPagingRequestModel>();

            CreateMap<AuthorizeRequestModel, AuthorizeModel>();
            CreateMap<AuthorizeModel, AuthorizeEntity>();
            CreateMap<AuthorizeEntity, AuthorizeModel>();
        }
    }
}
