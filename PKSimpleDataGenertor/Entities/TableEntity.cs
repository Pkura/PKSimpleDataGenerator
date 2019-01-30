using System.Collections.Generic;

namespace PKSimpleDataGenerator.Entities
{
    public class TableEntity
    {
        public string Name { get; set; }
        public int MaxRecordsToGenerate { get; set; }
        public bool TruncateTable { get; set; }
        public List<FieldEntity> Fields { get; set; } = new List<FieldEntity>();
    }
}