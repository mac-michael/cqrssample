using System.Collections.Generic;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public interface IProcessor<in TElement>
    {
        IEnumerable<XElement> Parse(IEnumerable<TElement> types);
    }
}