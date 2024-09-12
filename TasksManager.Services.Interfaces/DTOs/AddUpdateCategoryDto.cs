using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Services.DTOs
{
    public class AddUpdateCategoryDto : CategoryDto
    {
        public bool IsCreate { get; set; }
    }
}
