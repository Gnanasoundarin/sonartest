using MM.Suppliers.API.Common.Interfaces;
using MM.Suppliers.API.Common.Models;

namespace MM.Suppliers.API.WebApi.Tests.Fake
{
    public class SuppliersServiceFake : ISuppliersService
    {
        private readonly SuppliersPagingResponseModel _supplierPagingResponseModel;
        private readonly IEnumerable<SuppliersModel> _SuppliersModels;
        private readonly SuppliersModel _supplierModel;

        public SuppliersServiceFake()
        {
            _supplierModel = new SuppliersModel()
            {
                SupplierId = 1,
                SupplierName = "ABC Supplier1",
                ContactPerson = "Supplier Name1",
                ContactEmail = "Supplier Name1",
                Active = true
            };

            _SuppliersModels = new List<SuppliersModel>()
            {
                new SuppliersModel()
                {
                    SupplierId = 1,
                    SupplierName = "ABC Supplie1r2",
                    ContactPerson = "Supplier Name2",
                    ContactEmail = "Supplier Name2",
                    Active = true
                },
                new SuppliersModel()
                {
                    SupplierId = 1,
                    SupplierName = "ABC Supplie1r2",
                    ContactPerson = "Supplier Name2",
                    ContactEmail = "Supplier Name2",
                    Active = true
                }
            };

            _supplierPagingResponseModel = new SuppliersPagingResponseModel()
            {
                TotalRecordCount = 2,
                Data = _SuppliersModels
            };
        }

        public Task<SuppliersModel> GetAsync(int id)
        {
            return Task.FromResult(_supplierModel);
        }
        public Task<SuppliersModel> GetAllActiveAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<SuppliersPagingResponseModel> GetAllSuppliersPaginatedAsync(SuppliersPagingRequestModel pagingModel)
        {
            return Task.FromResult(_supplierPagingResponseModel);
        }

        public Task<SuppliersModel> GetByIdAsync(int id)
        {
            return Task.FromResult(_supplierModel);
        }

        public Task<SuppliersModel> InsertAsync(SuppliersModel model)
        {
            return Task.FromResult(_supplierModel);
        }

        public Task UpdateAsync(SuppliersModel model, int id)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            return Task.CompletedTask;
        }

        public Task<IEnumerable<SuppliersModel>> GetAllActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SuppliersModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SuppliersPagingResponseModel> GetAllPaginatedAsync(SuppliersPagingRequestModel pagingModel)
        {
            throw new NotImplementedException();
        }
    }
}
