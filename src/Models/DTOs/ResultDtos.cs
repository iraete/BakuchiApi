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

    public class UpdateResultDto : CreateResultDto
    { }

    public class ResultDtoMapper : DtoMapper<Result, ResultDto,
        UpdateResultDto, CreateResultDto>
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
            return new Result
            {
                EventId = dto.EventId,
                OutcomeId = dto.OutcomeId,
                Rank = dto.Rank
            };
        }

        public override Result MapCreateDtoToEntity(CreateResultDto dto)
        {
            return new Result
            {
                EventId = dto.EventId,
                OutcomeId = dto.OutcomeId,
                Rank = dto.Rank
            };
        }

        public override Result MapUpdateDtoToEntity(UpdateResultDto dto)
            => MapCreateDtoToEntity(dto);
    }
}