using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class CommandProcessor : ObjectProcessor
    {
        private readonly BuildingBlockRecognizer _recognizer;

        public CommandProcessor(BuildingBlockRecognizer recognizer)
            : base("Command", recognizer.IsCommand)
        {
            _recognizer = recognizer;
        }

        protected override IEnumerable<XElement> ParseChildren(Type type)
        {
            foreach (var handler in DomainProcessor.Current.GetTypes().Where(
                x => _recognizer.IsCommandHandler(x, type)))
            {
                yield return new XElement("Handler", new XAttribute("name", handler.Name));
            }
        }
    }
}