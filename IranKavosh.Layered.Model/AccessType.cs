using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations.Schema;

namespace IranKavosh.Layered.Model
{
    [Table(name: "AccessType", Schema = "Security")]
   
    public class AccessType : ModelId
    {
        public string Title { get; set; }

        [ForeignKey("AccessId")]
        public virtual ICollection<AccessList> AccessLists { get; set; }
    }
}