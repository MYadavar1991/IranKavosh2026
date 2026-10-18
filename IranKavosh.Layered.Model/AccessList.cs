using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Model
{
     [Table(name: "AccessList", Schema = "Security")]

    public class AccessList : ModelId
    {
        // کاربر
        public Guid PersonalId { get; set; }
        public Personal Personal { get; set; }
        // نام جدول
        public string Table { get; set; }

        // نمایش - حذف - ویرایش - آپدیت
        public Guid AccessTypeId { get; set; }
        public AccessType AccessType{ get; set; }
    }
}   