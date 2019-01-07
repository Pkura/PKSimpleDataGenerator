using PKSimpleDataGenerator.Enums;

namespace PKSimpleDataGenerator.Entities
{
    public class FieldEntity
    {
        public string Name { get; set; }
        public FieldTypeEnum FieldType { get; set; }
        public GeneratorTypeEnum GeneratorType { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int SomeNumber { get; set; }
    }


    
}
