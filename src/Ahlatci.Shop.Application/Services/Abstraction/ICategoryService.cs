using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.RequestModels;
using Ahlatci.Shop.Application.Wrapper;

namespace Ahlatci.Shop.Application.Services.Abstraction
{
    public interface ICategoryService
    {
        //Dto => Servisin dışarıya gönderdiği türler
        //VM => Servisin dışarıdan aldığı parametreler

        #region Select
        Task<Result<List<CategoryDto>>> GetAllCategories();
        Task<Result<CategoryDto>> GetCategoryById(GetCategoryByIdVM getCategoryByIdVM);

        #endregion

        #region Insert, Update, Delete
        Task<Result<int>> CreateCategory(CreateCategoryVM createCategoryVM);
        Task<Result<int>> UpdateCategory(UpdateCategoryVM updateCategoryVM);
        Task<Result<int>> DeleteCategory(DeleteCategoryVM deleteCategoryVM);
        #endregion
    }
}
