using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations.Schema;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "HddMemory", Schema = "Save")]
    public class HddMemory : ModelId
    {   
        public long TotalSize { get; set; }
        public string StableSerial { get; set; }
        [ForeignKey("HddMemoryId")]
        public virtual ICollection<HddMemoryPerson> HddMemoryPersons { get; set; }

        [ForeignKey("HddMemoryId")]
        public virtual ICollection<CopiedFilmToMemory> CopiedFilmToMemories { get; set; }
    }
}