using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class ObjectProcessor : IProcessor<Type>
    {
        private readonly string _objectName;
        private readonly Func<Type, bool> _predicate;

        public ObjectProcessor(string objectName, Func<Type,bool> predicate )
        {
            _objectName = objectName;
            _predicate = predicate;
        }

        public BuildingBlockRecognizer Recognizer { get; set; }

        public IEnumerable<XElement> Parse(IEnumerable<Type> types)
        {
            foreach (var type in types.Where(t => _predicate(t)))
            {
                yield return new XElement(_objectName, new XAttribute("name", type.Name), ParseChildren(type));
            }
        }

        protected virtual IEnumerable<XElement> ParseChildren(Type type)
        {
            yield break;
        }
    }
}