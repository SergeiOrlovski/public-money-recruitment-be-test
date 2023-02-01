using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Domain.Interfaces;
using VacationRental.Entities.Entities;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : BasePublicController
    {
        private readonly IRentalsService _rentalsService;
        public RentalsController(IRentalsService rentalsService, IMapper mapper) : base(mapper)
        {
            _rentalsService = rentalsService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public IActionResult Get(int rentalId)
        {
            var result = _rentalsService.GetRental(rentalId);

            return CreateResponse<Rental, RentalViewModel>(result);
        }

        [HttpPost]
        public IActionResult Post(RentalBindingModel model)
        {
            var rental = Mapper.Map<Rental>(model);
            
            var result = _rentalsService.CreateRental(rental);

            return CreateResponse<int, ResourceIdViewModel>(result);
        }
        
        [HttpPut]
        [Route("{rentalId:int}")]
        public IActionResult Put(int rentalId, RentalBindingModel model)
        {
            var rental = Mapper.Map<Rental>(model);
            
            var result = _rentalsService.UpdateRental(rentalId, rental);

            return CreateResponse<int, ResourceIdViewModel>(result);
        }
    }
}
