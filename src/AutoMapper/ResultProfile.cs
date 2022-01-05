using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.AutoMapper
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            CreateMap<Result, ResultDto>();
            CreateMap<ResultDto, Result>();
            CreateMap<CreateResultDto, Result>();
            CreateMap<UpdateResultDto, Result>();
        }
    }
}