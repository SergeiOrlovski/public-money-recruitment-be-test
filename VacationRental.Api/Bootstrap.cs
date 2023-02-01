using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using VacationRental.DataAccess.Interfaces;
using VacationRental.DataAccess.Repositories;
using VacationRental.Domain.Interfaces;
using VacationRental.Domain.Services;
using VacationRental.Domain.Validators;
using VacationRental.Entities.Entities;

namespace VacationRental.Api
{
    public static class Bootstrap
    {
        public static IServiceCollection AddVacationRentalService(this IServiceCollection services)
        {
            services.AddSingleton<IDictionary<int, Rental>>(new Dictionary<int, Rental>());
            services.AddSingleton<IDictionary<int, Booking>>(new Dictionary<int, Booking>());

            services.AddTransient<IRentalsRepository, RentalsRepository>();
            services.AddTransient<IBookingsRepository, BookingsRepository>();
            
            services.AddTransient<IRentalsService, RentalsService>();
            services.AddTransient<IBookingsService, BookingsService>();
            services.AddTransient<ICalendarService, CalendarService>();
            
            services.AddTransient<IRentalUpdatingValidator, RentalUpdatingValidator>();

            return services;
        }
    }
}