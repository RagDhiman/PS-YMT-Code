using AM_ExpensesManager_API.Models;
using AM_ExpensesManager_Core;
using AutoMapper;

namespace AM_ExpensesManager_API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<ExpenseModel, Expense>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<ExpenseLineModel, ExpenseLine>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
