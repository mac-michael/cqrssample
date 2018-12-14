using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class GroupingProcessor : CompositeProcessor<Type>, IProcessor<Type>
    {
        public GroupingProcessor(string name, params IProcessor<Type>[] subProcessors) : base(subProcessors)
        {
            _name = name;
        }

        private readonly string _name;

        public IEnumerable<XElement> Parse(IEnumerable<Type> types)
        {
            yield return new XElement(_name, ParseChildren(types));
        }
    }
}