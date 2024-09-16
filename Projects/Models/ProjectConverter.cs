using Shared.Dto;
using Shared.Interfaces;

namespace Projects.Models
{
    public class ProjectConverter : IConverter<Project, ProjectDto>
    {
        // Converts AddressDto to Address model
        public Project Convert(ProjectDto dto)
        {
            return new Project
            {
                Id = dto.Id,
                ProjectName = dto.ProjectName,
                ProjectDescription = dto.ProjectDescription,
                Priority = dto.Priority,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatedDate = dto.CreatedDate,
                LastUpdated = dto.LastUpdated,
                LastUpdatedBy = dto.LastUpdatedBy,
                CustomerId = dto.CustomerId,
                Customer = dto.Customer != null ? new CustomerConverter().Convert(dto.Customer) : null
            };
        }

        // Converts Address model to AddressDto
        public ProjectDto Convert(Project model)
        {
            return new ProjectDto
            {
                Id = model.Id,
                ProjectName = model.ProjectName,
                ProjectDescription = model.ProjectDescription,
                Priority = model.Priority,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreatedDate = model.CreatedDate,
                LastUpdated = model.LastUpdated,
                LastUpdatedBy = model.LastUpdatedBy,
                CustomerId = model.CustomerId,
                Customer = model.Customer != null ? new CustomerConverter().Convert(model.Customer) : null
            };
        }
    }
}
