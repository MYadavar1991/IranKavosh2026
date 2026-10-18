using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "ShareHolderDetail", Schema = "Stocks")]
    public class ShareHolderDetail : ModelId
    {
        public long Code { get; set; }

        public Guid PersonalId { get; set; }
        public virtual Personal Personal { get; set; }

        public Guid ShareHolderHeaderId { get; set; }
        public virtual ShareHolderHeader ShareHolderHeader { get; set; }


        /// <summary>
        /// سهام
        /// </summary>
        public float Stocks { get; set; }
    }
}