using AutoMapper;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;

namespace BakuchiApi.AutoMapper
{
    public class OutcomeProfile : Profile
    {
        public OutcomeProfile()
        {
            CreateMap<Outcome, OutcomeDto>();
            CreateMap<OutcomeDto, Outcome>();
            CreateMap<CreateOutcomeDto, Outcome>();
            CreateMap<UpdateOutcomeDto, Outcome>();
        }
    }
}