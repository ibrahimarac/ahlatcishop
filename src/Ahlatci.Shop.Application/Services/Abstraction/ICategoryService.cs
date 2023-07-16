using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.RequestModels;

namespace Ahlatci.Shop.Application.Services.Abstraction
{
    public interface ICategoryService
    {
        //Dto => Servisin dışarıya gönderdiği türler
        //VM => Servisin dışarıdan aldığı parametreler

        #region Select
        Task<List<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(int id);

        #endregion

        #region Insert, Update, Delete
        Task<int> CreateCategory(CreateCategoryVM createCategoryVM);
        Task<int> UpdateCategory(UpdateCategoryVM updateCategoryVM);
        Task<int> DeleteCategory(int id);
        #endregion
    }
}
