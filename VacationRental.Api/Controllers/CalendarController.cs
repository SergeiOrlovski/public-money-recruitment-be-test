using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Domain.Interfaces;
using VacationRental.Entities.Entities;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : BasePublicController
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService, IMapper mapper) : base(mapper)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        public IActionResult Get(int rentalId, DateTime start, int nights)
        {
            var result = _calendarService.GetCalendar(rentalId, start, nights);

            return CreateResponse<Calendar, CalendarViewModel>(result);
        }
    }
}
