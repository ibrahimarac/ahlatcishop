using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.Dtos.ProductImages;
using Ahlatci.Shop.Application.Models.RequestModels.ProductImages;
using Ahlatci.Shop.Application.Wrapper;

namespace Ahlatci.Shop.Application.Services.Abstraction
{
    public interface IProductImageService
    {
        #region Select

        Task<Result<List<ProductImageDto>>> GetAllImagesByProduct(GetAllProductImageByProductVM getByProductVM);
        Task<Result<List<ProductImageWithProductDto>>> GetAllProductImagesWithProduct(GetAllProductImageByProductVM getByProductVM);

        #endregion

        #region Insert, Update, Delete

        Task<Result<int>> CreateProductImage(CreateProductImageVM createProductImageVM);
        Task<Result<int>> DeleteProductImage(DeleteProductImageVM deleteProductImageVM);

        #endregion
    }
}
