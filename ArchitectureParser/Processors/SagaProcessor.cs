using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class SagaProcessor : ObjectProcessor
    {
        private readonly BuildingBlockRecognizer _recognizer;

        public SagaProcessor(BuildingBlockRecognizer recognizer)
            : base("Saga", recognizer.IsSaga)
        {
            _recognizer = recognizer;
        }

        protected override IEnumerable<XElement> ParseChildren(Type type)
        {
            foreach (var sagaEvent in _recognizer.GetEventsForSaga(type))
            {
                yield return new XElement("Event", new XAttribute("boundedContext", _recognizer.GetBoundedContextName(sagaEvent)),
                                          new XAttribute("name", sagaEvent.Name));
            }
        }
    }
}