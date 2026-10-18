using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "Unit", Schema = "Product")]
    public class Unit : ModelId
    {
        public string Title { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}