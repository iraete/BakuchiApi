using System.Collections.Generic;

namespace BakuchiApi.Models.Dtos
{
    public abstract class DtoMapper<TSource, TEntityDto>
    {
        public abstract TEntityDto MapEntityToDto(TSource entity);

        public virtual List<TEntityDto> MapEntitiesToDtos(
            List<TSource> entities)
        {
            var dtos = new List<TEntityDto>();
            foreach(var entity in entities)
            {
                dtos.Add(MapEntityToDto(entity));
            }

            return dtos;
        }

    }
}