using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchitectureParser
{
    public class AggregateProcessor : ObjectProcessor
    {
        public AggregateProcessor(BuildingBlockRecognizer recognizer)
            : base("Aggregate", recognizer.IsAggregateRoot)
        {
        }
    }
}