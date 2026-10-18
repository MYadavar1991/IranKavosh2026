using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;
using IranKavosh.Layered.Model.Tables.BOT;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "Personal", Schema = "Base")]
    public class Personal : ModelId
    {
        public long Code { get; set; }
        [Display(Name = "نام")]
        public string? FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }

        /// <summary>
        /// نام شرکت تأمین کننده
        /// </summary>
        public string? Company { get; set; }

        [StringLength(11)]
        public string? Mobile { get; set; }

        [StringLength(10)]
        public string CodeMelli { get; set; }

        public string? Username { get; set; }
        /// <summary>
        /// HashCode
        /// </summary>
        public string? Password { get; set; }
        public long InventoryMain { get; set; } 
        public long InventoryFree { get; set; } 
        public string? Address { get; set; } 
        public int MBUsable { get; set; } // حجم قابل مصرف

        public Guid PersonalGroupId { get; set; }
        public virtual PersonalGroup PersonalGroup { get; set; }

            
        #region بات بله

        public long BotUserId { get; set; }
        public DateTime? BotLoginDateTime { get; set; }

        #endregion

        [ForeignKey("PersonalId")]
        public virtual ICollection<DefinitiveAccount> DefinitiveAccounts { get; set; }
        [ForeignKey("PersonalId")]
        public virtual ICollection<MultiListPersonal> MultiListPersonals { get; set; }
        [ForeignKey("PersonalId")]
        public virtual ICollection<ShareHolderDetail> ShareHolderDetails { get; set; }
        [ForeignKey("PersonalId")]
        public virtual ICollection<AccessList> AccessLists { get; set; }
        [ForeignKey("PersonalId")]
        public virtual ICollection<HddMemoryPerson> HddMemoryPersons { get; set; }
        [ForeignKey("BuyerId")]
       
        public virtual ICollection<BFactorHeader> BFactorHeaders { get; set; }

        [ForeignKey("RegisterId")]
        public virtual ICollection<Fund> Funds { get; set; }

        [ForeignKey("PersonalId")]
        public virtual ICollection<Login> Logins { get; set; }

        [Display(Name = "مشخصات")]
        //[DisplayName( "مشخصات")]
        public string FullName => FirstName + " " + LastName;
    }
}