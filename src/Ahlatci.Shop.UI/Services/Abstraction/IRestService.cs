using RestSharp;

namespace Ahlatci.Shop.UI.Services.Abstraction
{
    public interface IRestService
    {
        Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(TRequest requestModel, string endpointUrl, bool tokenRequired = true);
    }
}
