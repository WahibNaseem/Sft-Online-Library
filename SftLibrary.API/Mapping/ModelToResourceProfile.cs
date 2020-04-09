using AutoMapper;
using SftLib.Data.Domain.Models;
using SftLibrary.API.Models;
using SftLibrary.API.Resources;
using SftLibrary.Data.Domain.Models;

namespace SftLibrary.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<UserForRegisterResource, User>().ReverseMap();
            CreateMap<UserForLoginResource, User>().ReverseMap();
            CreateMap<UserForListResource, User>().ReverseMap();

            CreateMap<Book, BookCheckoutResource>().ReverseMap();
            CreateMap<Book, BookResource>().ReverseMap();
            CreateMap<Status, StatusResource>().ReverseMap();
            CreateMap<SaveBookResource, Book>().ReverseMap();
            CreateMap<CheckoutHistory, CheckoutHistoryResource>().ReverseMap();
            

            CreateMap<User, UserForCheckoutResource>().ReverseMap();
        }
    }
}
