using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model
{
    [Table(name: "ErrorExcep", Schema = "LogE")]
    public class ErrorExcep:ModelId
    {
        //public Guid Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

        public ErrorExcep()
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.Now;
        }
    }
}