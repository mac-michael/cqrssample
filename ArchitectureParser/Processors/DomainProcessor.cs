using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class DomainProcessor : CompositeProcessor<Type>
    {
        [ThreadStatic]
        private static DomainProcessor _current;
        public static DomainProcessor Current
        {
            get { return _current; }
        }

        public DomainProcessor(params IProcessor<Type>[] subProcessors) : base(subProcessors)
        {
        }

        readonly List<Assembly> _assemblies = new List<Assembly>();
        public void AddAssemblyOfType<T>()
        {
            _assemblies.Add(typeof(T).Assembly);
        }

        public XElement Parse()
        {
            var types = GetTypes();

            try
            {
                _current = this;
                return new XElement("Domain", ParseChildren(types));
            }
            finally
            {
                _current = null;
            }
        }

        internal IEnumerable<Type> GetTypes()
        {
            return from a in _assemblies
                   from t in a.GetTypes()
                   select t;
        }
    }
}