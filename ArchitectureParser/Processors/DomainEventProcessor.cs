using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class DomainEventProcessor : ObjectProcessor
    {
        private readonly BuildingBlockRecognizer _recognizer;

        public DomainEventProcessor(BuildingBlockRecognizer recognizer)
            : base("Event", recognizer.IsDomainEvent)
        {
            _recognizer = recognizer;
        }

        protected override IEnumerable<XElement> ParseChildren(Type type)
        {
            foreach (var handler in DomainProcessor.Current.GetTypes())
            {
                if ( _recognizer.IsEventListener(handler, type) )
                {
                    yield return
                        new XElement("Listener",
                                     new XAttribute("boundedContext", _recognizer.GetBoundedContextName(handler)),
                                     new XAttribute("name", handler.Name));
                }

                if ( _recognizer.IsSagaHandlingEvent(handler,type))
                {
                    yield return
                        new XElement("Saga",
                                     new XAttribute("boundedContext", _recognizer.GetBoundedContextName(handler)),
                                     new XAttribute("name", handler.Name));
                }
            }
        }
    }
}