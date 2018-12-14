using System;

namespace CQRS.Base.Infrastructure.Attributes
{
    public class ComponentAttribute : Attribute
    {
        public ComponentLifestyle Lifestyle { get; set; }
        public bool Start { get; set; }

        public ComponentAttribute() : this(ComponentLifestyle.Singleton)
        {
        }

        public ComponentAttribute(ComponentLifestyle lifestyle) : this(lifestyle, false)
        {
        }

        public ComponentAttribute(ComponentLifestyle lifestyle, bool start)
        {
            Lifestyle = lifestyle;
            Start = start;
        }
    }
}
