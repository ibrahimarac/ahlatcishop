using Ahlatci.Shop.UI.Services.Abstraction;
using Newtonsoft.Json;
using RestSharp;

namespace Ahlatci.Shop.UI.Services.Implementation
{
    public class RestService : IRestService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public RestService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        #region Post İstekleri

        /// <summary>
        /// Post türündeki api metodlarına istek atmak için kullanılacak.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="requestModel"></param>
        /// <param name="endpointUrl"></param>
        /// <param name="tokenRequired"></param>
        /// <returns></returns>
        public async Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(TRequest requestModel, string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];
            var jsonModel = JsonConvert.SerializeObject(requestModel);

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Post);

            restRequest.AddParameter("application/json", jsonModel, ParameterType.RequestBody);
            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken()}");
            }

            var response = await restClient.ExecutePostAsync<TResponse>(restRequest);
            return response;
        }

        /// <summary>
        /// Post türündeki api metodlarına istek atmak için kullanılacak.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="requestModel"></param>
        /// <param name="endpointUrl"></param>
        /// <param name="tokenRequired"></param>
        /// <returns></returns>
        public async Task<RestResponse<TResponse>> PostAsync<TResponse>(string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Post);

            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken()}");
            }

            var response = await restClient.ExecutePostAsync<TResponse>(restRequest);
            return response;
        }

        #endregion

        #region Private Methods

        private string GetToken()
        {
            var sessionKey = _configuration["Application:SessionKey"];
            return _contextAccessor.HttpContext.Session.GetString(sessionKey);
        }

        #endregion
    }
}
