using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<UpdateUserInfoDto, User>();
        }
    }
}