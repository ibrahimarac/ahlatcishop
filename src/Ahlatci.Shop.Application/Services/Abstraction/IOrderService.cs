using Ahlatci.Shop.Application.Models.Dtos.Orders;
using Ahlatci.Shop.Application.Models.RequestModels.Orders;
using Ahlatci.Shop.Application.Wrapper;

namespace Ahlatci.Shop.Application.Services.Abstraction
{
    public interface IOrderService
    {
        #region Select
        Task<Result<List<OrderDto>>> GetOrdersByCustomer(GetOrdersByCustomerVM getOrdersByCustomerVM);

        #endregion

        #region Insert, Update, Delete
        Task<Result<int>> CreateOrder(CreateOrderVM createOrderVM);
        Task<Result<int>> UpdateOrder(UpdateOrderVM updateOrderVM);
        Task<Result<int>> DeleteOrder(DeleteOrderVM deleteOrderVM);
        #endregion
    }
}
