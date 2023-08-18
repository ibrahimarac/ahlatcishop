using Ahlatci.Shop.Application.Models.Dtos.OrderDetails;
using Ahlatci.Shop.Application.Models.RequestModels.OrderDetails;
using Ahlatci.Shop.Application.Models.RequestModels.Orders;
using Ahlatci.Shop.Application.Wrapper;

namespace Ahlatci.Shop.Application.Services.Abstraction
{
    public interface IOrderDetailService
    {
        #region Select
        Task<Result<List<OrderDetailDto>>> GetOrderDetailsByOrderId(GetOrderDetailsByOrderIdVM getByOrderIdVM);

        #endregion

        #region Insert, Update, Delete

        Task<Result<int>> CreateOrderDetail(CreateOrderDetailVM createOrderDetailVM);
        Task<Result<int>> DeleteOrderDetail(DeleteOrderDetailVM deleteOrderDetailVM);

        #endregion
    }
}
