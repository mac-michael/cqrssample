namespace ArchitectureParser
{
    public class RepositoryProcessor : ObjectProcessor
    {
        public RepositoryProcessor(BuildingBlockRecognizer recognizer)
            : base("Repository", recognizer.IsRepository)
        {
        }
    }
}