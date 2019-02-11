using PKSimpleDataGenerator.Entities;

namespace PKSimpleDataGenerator
{
    public interface IFieldDataGenerator
    {
        FieldEntity Field { get; set; }

        string GenerateData();


    }
}