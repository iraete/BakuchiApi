using System;

namespace BakuchiApi.Models.Dtos
{
    public class ResultDto
    {
        public Guid EventId { get; set; }
        public uint OutcomeId { get; set; }
        public uint Rank { get; set; }
        public DateTime LastEdited { get; set; }
    }

    public class CreateResultDto
    {
        public Guid EventId { get; set; }
        public uint OutcomeId { get; set; }
        public uint Rank { get; set; }
    }

    public class ResultDtoMapper : DtoMapper<Result, ResultDto>
    {
        public override ResultDto MapEntityToDto(Result r)
        {
            return new ResultDto
            {
                EventId = r.EventId,
                OutcomeId = r.OutcomeId,
                Rank = r.Rank,
                LastEdited = r.LastEdited
            };
        }

        public override Result MapDtoToEntity(ResultDto dto)
        {
            throw new NotImplementedException();
        }
    }
}