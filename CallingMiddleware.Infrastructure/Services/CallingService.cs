using CallingMiddleware.Application.Services;
using System.Text;


namespace CallingMiddleware.Infrastructure.Services
{
   
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CallingService : ICallingService
    {
        private readonly HttpClient _httpClient;

        public CallingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> CallRestServiceAsync<TResponse>(string url, HttpMethod method, dynamic requestBody)
        {
            var request = new HttpRequestMessage(method, url);

            // Convert the requestBody to a JSON string
            string requestBodyJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

            // Create StringContent using the JSON string
            request.Content = new StringContent(requestBodyJson, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize JSON response to the specified type TResponse
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(responseContent);

        }

        public async Task<string> CallSoapServiceAsync(string url, string soapAction, string requestBody)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("SOAPAction", soapAction);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "text/xml");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            


            return await response.Content.ReadAsStringAsync();
        }
    }


}
