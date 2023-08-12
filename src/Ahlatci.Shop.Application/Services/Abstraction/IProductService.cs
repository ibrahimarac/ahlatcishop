using Ahlatci.Shop.Application.Models.Dtos.Products;
using Ahlatci.Shop.Application.Models.RequestModels;
using Ahlatci.Shop.Application.Models.RequestModels.Products;
using Ahlatci.Shop.Application.Wrapper;

namespace Ahlatci.Shop.Application.Services.Abstraction
{
    public interface IProductService
    {
        #region Select

        Task<Result<List<ProductDto>>> GetAllProducts();
        Task<Result<List<ProductWithCategoryDto>>> GetAllProductsWithCategory();
        Task<Result<ProductDto>> GetProductById(GetProductByIdVM getProductByIdVM);

        #endregion

        #region Insert, Update, Delete

        Task<Result<int>> CreateProduct(CreateProductVM createProductVM);
        Task<Result<int>> UpdateProduct(UpdateProductVM updateProductVM);
        Task<Result<int>> DeleteProduct(DeleteProductVM deleteProductVM);

        #endregion
    }
}
