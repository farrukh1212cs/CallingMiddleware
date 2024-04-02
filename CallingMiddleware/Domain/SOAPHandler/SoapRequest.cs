using CallingMiddleware.Domain.AddInteger;

namespace CallingMiddleware.Domain.SOAPHandler
{
    public static class SoapRequest
    {

        //Sample Request
        public static string Register(AddIntegerRequest RequestDto)
        {
            var request = @$"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org"">
                                   <soapenv:Header/>
                                   <soapenv:Body>
                                      <tem:AddInteger>
                                         <!--Optional:-->
                                         <tem:Arg1>{RequestDto.Arg1}</tem:Arg1>
                                         <!--Optional:-->
                                         <tem:Arg2>{RequestDto.Arg2}</tem:Arg2>
                                      </tem:AddInteger>
                                   </soapenv:Body>
                                </soapenv:Envelope>";

            return request;
        }
    }
}
