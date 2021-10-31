using System.Collections.Generic;

namespace BakuchiApi.Controllers.Dtos
{
    public abstract class DtoMapper<TSource, TEntityDto,
        TUpdateEntityDto, TCreateEntityDto>
    {
        public abstract TEntityDto MapEntityToDto(TSource entity);
        public abstract TSource MapDtoToEntity(TEntityDto dto);
        public abstract TSource MapCreateDtoToEntity(TCreateEntityDto dto);
        public abstract TSource MapUpdateDtoToEntity(TUpdateEntityDto dto);

        public virtual List<TEntityDto> MapEntitiesToDtos(
            List<TSource> entities)
        {
            var dtos = new List<TEntityDto>();
            foreach (var entity in entities) dtos.Add(MapEntityToDto(entity));

            return dtos;
        }
    }
}