using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations.Schema;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "Login", Schema = "Base")]
    public class Login : ModelId
    {
        public string WinUser { get; set; }

        public Guid PersonalId { get; set; }    
        public Personal Personal { get; set; }    
    }
}    