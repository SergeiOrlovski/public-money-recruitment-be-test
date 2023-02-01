using AutoMapper;
using VacationRental.Api.Models;
using VacationRental.Entities.Entities;

namespace VacationRental.Api.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingViewModel>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.Id))
                .ForMember(x => x.Nights, src => src.MapFrom(x => x.Nights))
                .ForMember(x => x.Start, src => src.MapFrom(x => x.Start))
                .ForMember(x => x.RentalId, src => src.MapFrom(x => x.RentalId));
            
            CreateMap<Rental, RentalViewModel>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.Id))
                .ForMember(x => x.Units, src => src.MapFrom(x => x.UnitsCount))
                .ForMember(x => x.PreparationTimeInDays, src => src.MapFrom(x => x.PreparationTimeInDays));
            
            CreateMap<RentalBindingModel, Rental>()
                .ForMember(x => x.UnitsCount, src => src.MapFrom(x => x.Units))
                .ForMember(x => x.PreparationTimeInDays, src => src.MapFrom(x => x.PreparationTimeInDays))
                .ForMember(x => x.Units, src => src.Ignore());
            
            CreateMap<BookingBindingModel, Booking>()
                .ForMember(x => x.Nights, src => src.MapFrom(x => x.Nights))
                .ForMember(x => x.Start, src => src.MapFrom(x => x.Start))
                .ForMember(x => x.RentalId, src => src.MapFrom(x => x.RentalId));

            CreateMap<int, ResourceIdViewModel>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x));
            
            CreateMap<Calendar, CalendarViewModel>()
                .ForMember(x => x.RentalId, src => src.MapFrom(x => x.RentalId))
                .ForMember(x => x.Dates, src => src.MapFrom(x => x.Dates));
            
            CreateMap<CalendarDate, CalendarDateViewModel>()
                .ForMember(x => x.Date, src => src.MapFrom(x => x.Date))
                .ForMember(x => x.Bookings, src => src.MapFrom(x => x.Bookings))
                .ForMember(x => x.PreparationTimes, src => src.MapFrom(x => x.PreparationTimes));
            
            CreateMap<CalendarBooking, CalendarBookingViewModel>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.Id))
                .ForMember(x => x.Unit, src => src.MapFrom(x => x.Unit));
            
            CreateMap<Preparation, PreparationViewModel>()
                .ForMember(x => x.Unit, src => src.MapFrom(x => x.Unit));
        }
    }
}