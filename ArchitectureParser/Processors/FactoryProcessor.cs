namespace ArchitectureParser
{
    public class FactoryProcessor : ObjectProcessor
    {
        public FactoryProcessor(BuildingBlockRecognizer recognizer)
            : base("Factory", recognizer.IsFactory)
        {
        }
    }
}