using System.Collections.Generic;
using System.Xml.Serialization;

namespace Task.Utlities
{
    [XmlRoot(ElementName = "DataList")]
    public class DataList
    {
        [XmlElement(ElementName = "Website")]
        public List<Website> website { get; set; } = new List<Website>();

        [XmlElement(ElementName = "UPCs")]
        public List<UPCs> UPCs { get; set; } = new List<UPCs>();
    }

    [XmlRoot(ElementName = "Website")]
    public class Website
    {
        [XmlElement(ElementName = "Browser")]
        public string browser { get; set; }

        [XmlElement(ElementName = "BaseUrl")]
        public string url { get; set; }
    }

    [XmlRoot(ElementName = "UPCs")]
    public class UPCs
    {
        [XmlElement(ElementName = "UPC")]
        public List <string> UPC  = new List<string>();
    }
}
