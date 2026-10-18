using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "HddMemoryPerson", Schema = "Save")]
    public class HddMemoryPerson : ModelId
    {
        public Guid HddMemoryId { get; set; }
        public HddMemory HddMemory { get; set; }

        public Guid PersonalId { get; set; }
        public Personal Personal { get; set; }

    }
}   