using Ahlatci.Shop.UI.Exceptions;
using Ahlatci.Shop.UI.Models.Dtos.Accounts;
using Ahlatci.Shop.UI.Services.Abstraction;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

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
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
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
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion

        #region Get İstekleri

        /// <summary>
        /// Adres satırı üzerinden veri gönderilerek yapılan istekler
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="endpointUrl"></param>
        /// <param name="tokenRequired"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<RestResponse<TResponse>> GetAsync<TResponse>(string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Get);

            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion

        #region Delete İstekleri

        public async Task<RestResponse<TResponse>> DeleteAsync<TResponse>(string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Delete);

            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion

        #region Put İstekleri

        public async Task<RestResponse<TResponse>> PutAsync<TRequest, TResponse>(TRequest requestModel, string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];
            var jsonModel = JsonConvert.SerializeObject(requestModel);

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Put);

            restRequest.AddParameter("application/json", jsonModel, ParameterType.RequestBody);
            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion



        #region Private Methods

        private TokenDto GetToken()
        {
            var sessionKey = _configuration["Application:SessionKey"];
            if (_contextAccessor.HttpContext.Session.GetString(sessionKey) is null)
                return null;
            var tokenDto = JsonConvert.DeserializeObject<TokenDto>(_contextAccessor.HttpContext.Session.GetString(sessionKey));
            return tokenDto;
        }

        private void CheckResponse<TResponse>(RestResponse<TResponse> response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthenticatedException();
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedException();
            }
        }

        public async Task<RestResponse<TResponse>> PostFormAsync<TResponse>(Dictionary<string, string> formValues, string endpointUrl, IFormFile file, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Post);

            
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "multipart/form-data");

            foreach (var formValueKey in formValues.Keys)
            {
                restRequest.AddParameter(formValueKey, formValues[formValueKey]);
            }

            if (file != null)
            {
                restRequest.AddFile("UploadedImage", file.FileName);
            }            

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion
    }
}
