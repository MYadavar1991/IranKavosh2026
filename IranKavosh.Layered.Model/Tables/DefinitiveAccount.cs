using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{

    [Table(name: "DefinitiveAccount", Schema = "Account")]
    public class DefinitiveAccount : ModelId
    {
        public long Code{ get; set; }
        /// <summary>
        /// اشتراکی بین جداول مالی
        /// </summary>
        public long DocNumber { get; set; }
        public Guid PersonalId { get; set; }    
        public virtual Personal Personal { get; set; }
        public DateTime DateCustom { get; set; }
        public long Price { get; set; }

        /// <summary>
        /// بدهکار یا بستانکار
        /// </summary>
        public bool Debtor { get; set; }
        public string? Description { get; set; }
    }
}