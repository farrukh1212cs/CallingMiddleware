using CallingMiddleware.Domain.AddInteger;

namespace CallingMiddleware.Domain.SOAPHandler.Helper
{
    public static class XmlMapper
    {
        public static AddIntegerResponse AddIntegerMapping(List<XMLTag> tags)
        {
            AddIntegerResponse response = new AddIntegerResponse();
            response.AddIntegerResult = tags.FirstOrDefault(x => x.tag_name == "AddIntegerResult")?.tag_value ?? "";           
            return response;
        }

       
    }
}
