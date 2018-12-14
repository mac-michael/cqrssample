namespace ArchitectureParser
{
    public class ValueObjectProcessor : ObjectProcessor
    {
        public ValueObjectProcessor(BuildingBlockRecognizer recognizer) : base("ValueObject", recognizer.IsValueObject)
        {
        }
    }
}