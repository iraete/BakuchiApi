using System.Collections.Generic;
using System.Threading.Tasks;

namespace BakuchiApi.Models.Dtos
{
    public abstract class DtoMapper<TSource, TEntityDto>
    {
        public abstract TEntityDto MapEntityToDto(TSource entity);
        public abstract TSource MapDtoToEntity(TEntityDto dto);

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