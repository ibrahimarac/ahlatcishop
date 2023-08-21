using RestSharp;

namespace Ahlatci.Shop.UI.Services.Abstraction
{
    public interface IRestService
    {
        Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(TRequest requestModel, string endpointUrl, bool tokenRequired = true);

        Task<RestResponse<TResponse>> PostAsync<TResponse>(string endpointUrl, bool tokenRequired = true);

        Task<RestResponse<TResponse>> PostFormAsync<TResponse>(Dictionary<string, string> parameters, string endpointUrl, bool tokenRequired = true);

        Task<RestResponse<TResponse>> GetAsync<TResponse>(string endpointUrl, bool tokenRequired = true);

        Task<RestResponse<TResponse>> DeleteAsync<TResponse>(string endpointUrl, bool tokenRequired = true);

        Task<RestResponse<TResponse>> PutAsync<TRequest,TResponse>(TRequest requestModel, string endpointUrl, bool tokenRequired = true);


    }
}
