using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.AutoMapper
{
    public class PoolProfile : Profile
    {
        public PoolProfile()
        {
            CreateMap<Pool, PoolDto>();
            CreateMap<PoolDto, Pool>();
            CreateMap<CreatePoolDto, Pool>();
            CreateMap<UpdatePoolDto, Pool>();
        }
    }
}