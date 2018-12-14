namespace ArchitectureParser
{
    public class ServiceProcessor : ObjectProcessor
    {
        public ServiceProcessor(BuildingBlockRecognizer recognizer)
            : base("Service", recognizer.IsService)
        {
        }
    }
}