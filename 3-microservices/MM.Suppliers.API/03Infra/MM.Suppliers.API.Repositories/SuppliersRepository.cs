using AutoMapper;
using MM.Base.Core.Repositories;
using MM.Suppliers.API.Common.Interfaces;
using MM.Suppliers.API.Common.Models;
using MM.Suppliers.API.Domain.Entities;
using System.Text;

namespace MM.Suppliers.API.Repositories
{

    public class SuppliersRepository : BaseRepository<Supplier>, ISuppliersRepository
    {


        public SuppliersRepository(DapperContext<Supplier> context, IMapper mapper) : base(context, mapper)
        {

        }


        public async Task<IEnumerable<SuppliersModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<SuppliersModel>>(await _context.GetAllAsync(SuppliersSqlQueryConstant.GetSuppliersQuery));
        }

        public async Task<SuppliersModel> GetAsync(int id)
        {
            var parameters = new { SupplierId = id };

            var SqlQuery = new StringBuilder(SuppliersSqlQueryConstant.GetSuppliersQuery);
            SqlQuery.Append(" WHERE [SupplierId] = @SupplierId ORDER BY[SupplierName],[ContactPerson] ");

            var data = await _context.GetSingleOrDefaultByParamAsync(SqlQuery.ToString(), parameters);
            return _mapper.Map<SuppliersModel>(data);
        }

        public async Task<SuppliersModel> InsertAsync(SuppliersModel entity)
        {
            entity.SupplierName = entity.SupplierName.Trim();
            entity.ContactPerson = entity.ContactPerson.Trim();
            entity.ContactEmail = entity.ContactEmail.Trim();
            var parameters = new { SupplierName = entity.SupplierName, ContactEmail = entity.ContactEmail };
            var SqlQuery = new StringBuilder(SuppliersSqlQueryConstant.GetSuppliersQuery);
            SqlQuery.Append(" WHERE [SupplierName] = @SupplierName AND [ContactEmail] = @ContactEmail  ");
            var checkSupplierExists = await _context.GetSingleOrDefaultByParamAsync(SqlQuery.ToString(), parameters);
            if (checkSupplierExists != null)
            {
                throw new Exception("Supplier is already exists!!!!");
            }
            else
            {

                entity.Active = true;
                return _mapper.Map<SuppliersModel>(await _context.InsertAsync(SuppliersSqlQueryConstant.InsertSqlQuery, _mapper.Map<Supplier>(entity)));
            }
        }

        public async Task UpdateAsync(SuppliersModel entity, int id)
        {
            entity.SupplierName = entity.SupplierName.Trim();
            entity.ContactPerson = entity.ContactPerson.Trim();
            entity.ContactEmail = entity.ContactEmail.Trim();
            var parameters = new { SupplierName = entity.SupplierName, ContactEmail = entity.ContactEmail };
            var SqlQuery = new StringBuilder(SuppliersSqlQueryConstant.GetSuppliersQuery);
            SqlQuery.Append(" WHERE [SupplierName] = @SupplierName AND [ContactEmail] = @ContactEmail  ");
            var checkSupplierExists = await _context.GetSingleOrDefaultByParamAsync(SqlQuery.ToString(), parameters);
            entity.SupplierId = id;

            if (checkSupplierExists != null)
            {
                if (entity.SupplierId != checkSupplierExists.SupplierId)
                    throw new Exception("Supplier is already exists!!!!");
                else await _context.UpdateAsync(SuppliersSqlQueryConstant.UpdateSqlQuery, _mapper.Map<Supplier>(entity));
            }
            else
            {
                await _context.UpdateAsync(SuppliersSqlQueryConstant.UpdateSqlQuery, _mapper.Map<Supplier>(entity));
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _context.DeleteAsync(SuppliersSqlQueryConstant.DeleteSqlQuery, id);
        }
        public async Task<SuppliersPagingResponseModel> GetAllPaginatedAsync(SuppliersPagingRequestModel pagingModel)
        {
            if (pagingModel.Active)
            {
                pagingModel.Search.Add("Active:1");
            }

            if (string.IsNullOrEmpty(pagingModel.SortColumn))
            {
                pagingModel.SortColumn = "SupplierName,ContactPerson";
            }
            pagingModel.AllowedColumnsList = SuppliersSqlQueryConstant.GetSuppliersAllowedColumnListForSearch();
            var response = await _context.GetAllSearchPaginatedAsync<SuppliersPagingRequestModel>(SuppliersSqlQueryConstant.GetAllSqlQuery, SuppliersSqlQueryConstant.GetAllCountSqlQuery, pagingModel);
            return response == null
                ? new SuppliersPagingResponseModel()
                : new SuppliersPagingResponseModel
                {
                    Data = _mapper.Map<IList<SuppliersModel>>(response.Data),
                    TotalRecordCount = response.TotalRecordCount,
                };
        }
    }
}
