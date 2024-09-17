using Shared.Dto;
using Shared.Interfaces;

namespace Economy.Models
{
    public class MaterialConverter : IConverter<Material, MaterialDto>
    {
        // Converts MaterialDto to Material model
        public Material Convert(MaterialDto dto)
        {
            return new Material
            {
                Id = dto.Id,
                MaterialType = dto.MaterialType,
                Price = dto.Price,
                ProjectEcoId = dto.ProjectEcoId
            };
        }

        // Converts Material model to MaterialDto
        public MaterialDto Convert(Material model)
        {
            return new MaterialDto
            {
                Id = model.Id,
                MaterialType = model.MaterialType,
                Price = model.Price,
                ProjectEcoId = model.ProjectEcoId
            };
        }
    }
}
