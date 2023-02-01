using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Domain.Interfaces;
using VacationRental.Entities.Entities;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : BasePublicController
    {
        private readonly IBookingsService _bookingService;
        public BookingsController(IBookingsService bookingService, IMapper mapper) : base(mapper)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public IActionResult Get(int bookingId)
        {
            var result = _bookingService.GetBooking(bookingId);

            return CreateResponse<Booking, BookingViewModel>(result);
        }

        [HttpPost]
        public IActionResult Post(BookingBindingModel model)
        {
            var booking = Mapper.Map<Booking>(model);
            
            var result = _bookingService.CreateBooking(booking);

            return CreateResponse<int, ResourceIdViewModel>(result);
        }
    }
}
