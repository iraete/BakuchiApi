using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.AutoMapper
{
    public class WagerProfile : Profile
    {
        public WagerProfile()
        {
            CreateMap<Wager, WagerDto>();
            CreateMap<WagerDto, Wager>();
            CreateMap<CreateWagerDto, Wager>();
            CreateMap<UpdateWagerDto, Wager>();
        }
    }
}