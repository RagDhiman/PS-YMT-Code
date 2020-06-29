using AM_NotificationsService_API.Models;
using AM_NotificationsService_Core;
using AutoMapper;

namespace AM_NotificationsService_API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<EmailModel, Email>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SMSModel, SMS>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<WebhookPostModel, WebhookPost>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
