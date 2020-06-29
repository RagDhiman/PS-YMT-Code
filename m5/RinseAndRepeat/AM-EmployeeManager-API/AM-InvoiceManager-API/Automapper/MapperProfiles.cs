using AM_EmployeeManager_API.Models;
using AM_EmployeeManager_Core;
using AutoMapper;

namespace AM_EmployeeManager_API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<EmployeeModel, Employee>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<AbsenceModel, Absence>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<EquipmentModel, Equipment>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<HolidayModel, Holiday>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();


            this.CreateMap<PayModel, Pay>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<TaxInformationModel, TaxInformation>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<TrainingModel, Training>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
