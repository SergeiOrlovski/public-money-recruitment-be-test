using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Entities.Result;

namespace VacationRental.Api.Controllers
{
    public class BasePublicController : ControllerBase
    {
        protected readonly IMapper Mapper;
        
        public BasePublicController(IMapper mapper) => Mapper = mapper;
        
        protected IActionResult CreateResponse<TData, TPayload>(DataResult<TData> result) where TPayload : class
        {
            return result?.IsError == true 
                ? BadResponse(result.Error)
                : OkResponse(Mapper.Map<TPayload>(result.Data));
        }
        
        private IActionResult OkResponse<TPayload>(TPayload payload = default) where TPayload : class
            => Ok(payload);

        private IActionResult BadResponse(string errorCode)
            => BadRequest(errorCode);
    }
}