using AutoMapper;
using SftLib.Data.Domain.Models;
using SftLibrary.API.Models;
using SftLibrary.API.Resources;

namespace SftLibrary.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<UserForRegisterResource, User>();
            CreateMap<UserForLoginResource, User>();

            CreateMap<Book, BookResource>();
            CreateMap<Status, StatusResource>();
            CreateMap<SaveBookResource, Book>();
        }
    }
}
