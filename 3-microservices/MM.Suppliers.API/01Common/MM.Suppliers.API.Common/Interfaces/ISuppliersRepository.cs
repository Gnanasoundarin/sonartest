using MM.Base.Core.Repositories;
using MM.Suppliers.API.Common.Models;

namespace MM.Suppliers.API.Common.Interfaces
{
    public interface ISuppliersRepository : IBaseRepository<SuppliersModel, SuppliersPagingRequestModel, SuppliersPagingResponseModel>
    {       
    }
}
