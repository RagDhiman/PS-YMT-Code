using AM_SupplierManager_API.Models;
using AM_SupplierManager_Core;
using AutoMapper;

namespace AM_SupplierManager_API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<SupplierModel, Supplier>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SupplierNoteModel, SupplierNote>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<AttachmentModel, Attachment>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
