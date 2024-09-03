using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Services.DTOs;

namespace TasksManager.Services.Interfaces.RepositoryServices
{
    public interface ICategoryRepositoryCommandService
    {
        Task CreateCategory(AddUpdateCategoryDto addUpdateCategoryDto);
    }
}
