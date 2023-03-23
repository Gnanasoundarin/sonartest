using AutoMapper;
using MM.Base.Core.ApiModels;
using MM.Base.Core.Models;
using MM.Base.Core.Services;
using MM.Suppliers.API.Web.APIModels;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;


namespace MM.Suppliers.API.Web.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration Configuration;
        public JwtMiddleware(IConfiguration configuration, RequestDelegate next)
        {
            _next = next;
            Configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IAuthorizeService authorizeService, IMapper mapper)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            try
            {
                if (token == null)
                    throw new Exception();

                var tokenHandler = new JwtSecurityTokenHandler();
                var accessToken = tokenHandler.ReadJwtToken(token);

                var emailID = accessToken.Claims.First(a => a.Type.Contains("upn")).Value;

                if (!string.IsNullOrEmpty(emailID))
                {
                    context.Items["EmailID"] = emailID;
                }

                var requestAuthorizeModel = new AuthorizeRequestModel();
                requestAuthorizeModel.Email = emailID;
                var authorizeModel = await authorizeService.GetAllAsync(mapper.Map<AuthorizeModel>(requestAuthorizeModel));
                if (authorizeModel.Active == false && authorizeModel.RoleName != "Guest")
                {
                    await HandleExceptionMessageAsync(context, "User is Inactive.").ConfigureAwait(false);
                }
                else
                {
                    context.Items["UserID"] = authorizeModel.UserID;
                    context.Items["RoleName"] = authorizeModel.RoleName;
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex.Message.ToString()).ConfigureAwait(false);
            }

            await _next(context);
        }
        private static Task HandleExceptionMessageAsync(HttpContext context, string message)
        {
            if (message == "")
            {
                message = "Access Denied.";
            }
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.Unauthorized;
            var result = JsonConvert.SerializeObject(new BaseResponseModel<BaseModel>().OnError(null, "Access Denied. Please contact Maintenance Manager Administrator."));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}