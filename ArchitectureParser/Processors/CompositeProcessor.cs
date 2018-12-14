using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class CompositeProcessor<TElement>
    {
        public CompositeProcessor( params IProcessor<TElement>[] subProcessors )
        {
            SubProcessors = new List<IProcessor<TElement>>(subProcessors);
        }

        protected IEnumerable<XElement> ParseChildren(IEnumerable<TElement> elements)
        {
            var e = Enumerable.Empty<XElement>();
            foreach (var processor in SubProcessors)
                e = e.Union(processor.Parse(elements));

            return e;
        }

        public List<IProcessor<TElement>> SubProcessors { get; private set; }
    }
}