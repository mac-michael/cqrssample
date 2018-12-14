using System.Linq;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public static class XmlHelpers
    {
        public static XElement WithDefaultXmlNamespace(this XElement element, XNamespace xmlns)
        {
            if (element.Name.NamespaceName == string.Empty)
                element.Name = xmlns + element.Name.LocalName;

            foreach (var subElements in element.Elements())
                subElements.WithDefaultXmlNamespace(xmlns);

            return element;
        }
    }
}