namespace CallingMiddleware.Domain.SOAPHandler.Helper
{
    public class XMLTag
    {
        public string tag_name { get; set; }
        public string tag_value { get; set; }
        public List<XMLTag> childs { get; set; }
    }
}
