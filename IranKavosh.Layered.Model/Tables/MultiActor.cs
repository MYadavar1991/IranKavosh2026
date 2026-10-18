using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiActor", Schema = "Movie")]
    public class MultiActor : ModelId
    {
        public string ActorName { get; set; }
        public string ActorNameEn { get; set; }

    }
}