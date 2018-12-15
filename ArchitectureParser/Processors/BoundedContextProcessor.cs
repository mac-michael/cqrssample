using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class BoundedContextProcessor : CompositeProcessor<Type>, IProcessor<Type>
    {
        public BoundedContextProcessor(BuildingBlockRecognizer recognizer, params IProcessor<Type>[] subProcessors) : base(subProcessors)
        {
            _recognizer = recognizer;
        }

        private readonly BuildingBlockRecognizer _recognizer;


        public IEnumerable<XElement> Parse(IEnumerable<Type> types)
        {
            var contexts = from t in types
                           let bc = _recognizer.GetBoundedContextName(t)
                           group t by bc
                           into g
                           where g.Key != ""
                           select g;

            foreach (var bc in contexts)
            {
                yield return new XElement("BoundedContext",
                                          new XAttribute("name", bc.Key),
                                          ParseChildren(bc));
            }
        }
    }
}