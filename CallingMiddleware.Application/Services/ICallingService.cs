using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallingMiddleware.Application.Services
{
    public interface ICallingService
    {
        Task<TResponse> CallRestServiceAsync<TResponse>(string url, HttpMethod method, dynamic model);
        Task<string> CallSoapServiceAsync(string url, string soapAction, string requestBody);
    }

}
