using MM.Base.Core.Services;
using MM.Suppliers.API.Common.Models;

namespace MM.Suppliers.API.Common.Interfaces
{
    public interface ISuppliersService : IBaseService<SuppliersModel, SuppliersPagingRequestModel, SuppliersPagingResponseModel>
    {        
    }
}
