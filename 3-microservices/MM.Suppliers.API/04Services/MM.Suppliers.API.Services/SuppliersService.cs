using Microsoft.AspNetCore.Http;
using MM.Suppliers.API.Common.Interfaces;
using MM.Suppliers.API.Common.Models;

namespace MM.Suppliers.API.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IAzureService _azureService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public SuppliersService(IHttpContextAccessor httpContextAccessor, ISuppliersRepository suppliersRepository, IAzureService azureService)
        {
            _suppliersRepository = suppliersRepository;
            _azureService = azureService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<SuppliersModel>> GetAllAsync()
        {
            return await _suppliersRepository.GetAllAsync();
        }      
        public async Task<SuppliersModel> GetAsync(int id)
        {
            return await _suppliersRepository.GetAsync(id);
        }

        public async Task<SuppliersModel> InsertAsync(SuppliersModel entity)
        {
            entity.InsertedBy = Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserID"]);
            return await _suppliersRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(SuppliersModel entity, int id)
        {
            entity.UpdatedBy = Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserID"]);
            await _suppliersRepository.UpdateAsync(entity, id);
        }

        public async Task DeleteAsync(int id)
        {
            await _suppliersRepository.DeleteAsync(id);
        }
        public async Task<SuppliersPagingResponseModel> GetAllPaginatedAsync(SuppliersPagingRequestModel pagingModel)
        {
            return await _suppliersRepository.GetAllPaginatedAsync(pagingModel);
        }
    }
}
