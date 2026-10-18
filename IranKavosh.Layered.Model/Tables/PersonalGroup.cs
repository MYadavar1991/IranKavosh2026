using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "PersonalGroup", Schema = "Base")]
    public class PersonalGroup : ModelId
    {
        public string Title { get; set; }

        public virtual ICollection<Personal> Personals { get; set; }
    }
}