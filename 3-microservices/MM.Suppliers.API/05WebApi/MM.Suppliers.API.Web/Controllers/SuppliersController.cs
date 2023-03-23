using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MM.Suppliers.API.Common.Interfaces;
using MM.Suppliers.API.Common.Models;

using MM.Suppliers.API.Web.APIModels;
using MM.Suppliers.API.Web.Filters;
using System.Net.Mime;

namespace MM.Suppliers.API.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuppliersController : ControllerBase
    {

        private readonly ISuppliersService _suppliersService;
        private readonly ILogger<SuppliersController> _logger;
        private readonly IMapper _mapper;


        public SuppliersController(
            ILogger<SuppliersController> logger,
            ISuppliersService suppliersService
           , IMapper mapper)
        {
            _suppliersService = suppliersService;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Role.Administrator, Role.Normal)]
        [HttpGet]
        [ProducesResponseType(typeof(SuppliersApiPagingResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllSuppliersPaginatedAsync([FromQuery] SuppliersApiPagingRequestModel suppliersApiPagingRequestModel)
        {
            SuppliersPagingResponseModel response = await _suppliersService.GetAllPaginatedAsync(_mapper.Map<SuppliersPagingRequestModel>(suppliersApiPagingRequestModel));
            return Ok(new SuppliersApiPagingResponseModel().OnSuccess(response.Data.ToList(),
                response.TotalRecordCount,
                suppliersApiPagingRequestModel.PageNumber,
                suppliersApiPagingRequestModel.Limit,
                "Suppliers fetched successfully.",
                "Suppliers fetched"));
        }



        [Authorize(Role.Administrator)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var suppliersModels = await _suppliersService.GetAsync(id);
            return Ok(new SuppliersResponseModel().OnSuccess(new
            {
                SupplierId = suppliersModels.SupplierId,
                SupplierName = suppliersModels.SupplierName,
                ContactPerson = suppliersModels.ContactPerson,
                ContactEmail = suppliersModels.ContactEmail,
                Active = suppliersModels.Active
            }, "Supplier fetched successfully.", "Supplier fetched."));
        }

        [Authorize(Role.Administrator)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(SuppliersCreateRequestModel suppliersCreateRequestModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var model = await _suppliersService.InsertAsync(_mapper.Map<SuppliersModel>(suppliersCreateRequestModel));
                    return Ok(new SuppliersResponseModel().OnSuccess(new { Id = model.SupplierId },
                       suppliersCreateRequestModel.SupplierName + " has been created successfully.",
                       "Supplier created."));
                }
                else
                {
                    return UnprocessableEntity(ModelState);

                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SuppliersResponseModel().OnError(suppliersCreateRequestModel,
                    ex.Message,
                    ex.Message));
            }

        }

        [Authorize(Role.Administrator)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(SuppliersUpdateRequestModel suppliersUpdateRequestModel, int id)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    await _suppliersService.UpdateAsync(_mapper.Map<SuppliersModel>(suppliersUpdateRequestModel), id);
                    return Ok(new SuppliersResponseModel().OnSuccess(new { Id = id },
                              suppliersUpdateRequestModel.SupplierName + " has been updated successfully.",
                              "Supplier updated."));
                }
                else
                {
                    return UnprocessableEntity(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SuppliersResponseModel().OnError(suppliersUpdateRequestModel,
                    ex.Message,
                    ex.Message));
            }
        }

        [Authorize(Role.Administrator)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                await _suppliersService.DeleteAsync(id);
                return Ok(new SuppliersResponseModel().OnSuccess(null, "Supplier has been deleted successfully.", "Supplier deleted."));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new SuppliersResponseModel().OnError(null,
                   "The suppliers cannot be deleted because it is associated with other tables",
                   ex.Message));
            }
        }

    }
}