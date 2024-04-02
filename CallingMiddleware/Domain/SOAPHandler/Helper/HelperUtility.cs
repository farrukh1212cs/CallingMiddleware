using Newtonsoft.Json;
using System.Xml.Linq;

namespace CallingMiddleware.Domain.SOAPHandler.Helper
{
    public static class HelperUtility
    {
        
        public static string XmlToJson(string xml)
        {
            var doc = XDocument.Parse(xml);
            return JsonConvert.SerializeXNode(doc);
        }

        public static List<XMLTag> MapXmlNode(string saop_res, string? name, string? namespce)
        {
            var session_response = XName.Get(name, namespce);

            IEnumerable<XElement> elements = XDocument.Parse(saop_res).Descendants(session_response).Elements();

            List<XMLTag> data_tags = new List<XMLTag>();
            elements.ToList().ForEach(e =>
            {

                if (!e.HasElements)
                {
                    data_tags.Add(new XMLTag { tag_name = e.Name.LocalName, tag_value = e.Value });
                }
                else
                {
                    List<XMLTag> trans_info = new List<XMLTag>();

                    e.Elements().ToList().ForEach(tinfo =>
                    {
                        if (!tinfo.HasElements)
                        {
                            trans_info.Add(new XMLTag { tag_name = tinfo.Name.LocalName, tag_value = tinfo.Value });
                        }
                        else
                        {
                            List<XMLTag> trans_info_detail = new List<XMLTag>();
                            tinfo.Elements().ToList().ForEach(td =>
                            {
                                if (!td.HasElements)
                                {
                                    trans_info_detail.Add(new XMLTag { tag_name = td.Name.LocalName, tag_value = td.Value, childs = null });
                                }
                                else
                                {
                                    List<XMLTag> bonus = new List<XMLTag>();
                                    td.Elements().ToList().ForEach(td => {
                                        bonus.Add(new XMLTag { tag_name = td.Name.LocalName, tag_value = td.Value });
                                    });
                                    trans_info_detail.Add(new XMLTag { tag_name = td.Name.LocalName, tag_value = td.Value, childs = bonus });


                                }
                            });
                            trans_info.Add(new XMLTag { tag_name = tinfo.Name.LocalName, tag_value = tinfo.Value, childs = trans_info_detail });
                        }

                    });
                    data_tags.Add(new XMLTag { tag_name = e.Name.LocalName, tag_value = e.Value, childs = trans_info });
                }
            });

            return data_tags;
        }
    }
}
