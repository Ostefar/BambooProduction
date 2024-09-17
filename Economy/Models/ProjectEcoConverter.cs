using Shared.Dto;
using Shared.Interfaces;

namespace Economy.Models
{
    public class ProjectEcoConverter : IConverter<ProjectEco, ProjectEcoDto>
    {
        // Converts ProjectEcoDto to ProjectEco model
        public ProjectEco Convert(ProjectEcoDto dto)
        {
            return new ProjectEco
            {
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                TotalCost = dto.TotalCost,
                FixedPrice = dto.FixedPrice,
                HoursTotal = dto.HoursTotal,
                MaterialsPriceTotal = dto.MaterialsPriceTotal,
                CreatedDate = dto.CreatedDate,
                LastUpdated = dto.LastUpdated,
                LastUpdatedBy = dto.LastUpdatedBy
            };
        }

        // Converts ProjectEco model to ProjectEcoDto
        public ProjectEcoDto Convert(ProjectEco model)
        {
            return new ProjectEcoDto
            {
                Id = model.Id,
                ProjectId = model.ProjectId,
                TotalCost = model.TotalCost,
                FixedPrice = model.FixedPrice,
                HoursTotal = model.HoursTotal,
                MaterialsPriceTotal = model.MaterialsPriceTotal,
                CreatedDate = model.CreatedDate,
                LastUpdated = model.LastUpdated,
                LastUpdatedBy = model.LastUpdatedBy

            };
        }
    }
}
