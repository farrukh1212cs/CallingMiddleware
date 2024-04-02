using CallingMiddleware.Application.Services;
using CallingMiddleware.Domain.AddInteger;
using CallingMiddleware.Domain.Login;
using CallingMiddleware.Domain.SOAPHandler;
using CallingMiddleware.Domain.SOAPHandler.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace CallingMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesterController : ControllerBase
    {

        private readonly ICallingService _callingService;
        public TesterController(ICallingService callingService)
        {
            _callingService = callingService;
        }

        [HttpPost("AddInteger")]
        public async Task<IActionResult> AddIntegerRequest(AddIntegerRequest Dto)
        {
            string request = SoapRequest.Register(Dto);
            string SoapAction = "http://tempuri.org/SOAP.Demo.AddInteger";
            string url = "https://www.crcind.com/csp/samples/SOAP.Demo.CLS";

            var result = await _callingService.CallSoapServiceAsync(url, SoapAction, request);

            var xmldata = HelperUtility.MapXmlNode(result, "AddIntegerResponse", "http://tempuri.org");

            var Mappedresponse = XmlMapper.AddIntegerMapping(xmldata);

           
            return Ok(Mappedresponse);

        }

        [HttpPost("RestTesting")]
        public async Task<IActionResult> RestTesting(LoginRequest Dto)
        {
            string restURL = "";

            var result = await _callingService.CallRestServiceAsync<dynamic>(restURL, HttpMethod.Post, Dto);

            // Serialize the dynamic object to JSON
            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);

            // Return the JSON string
            return Content(jsonResult, "application/json");
        }
    }
}
