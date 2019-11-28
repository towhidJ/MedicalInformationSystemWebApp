using System.Web.Mvc;

namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReceptionTB")]
    public partial class ReceptionTB
    {
        PasswordHelper passwordHelper = new PasswordHelper();
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [NotMapped]
        public string NameED
        {
            get { return passwordHelper.AesDecryption(Name); }
            set { }
        }
        [StringLength(50)]
        public string Phone { get; set; }
        [NotMapped]
        public string PhoneED
        {
            get { return passwordHelper.AesDecryption(Phone); }
            set { }
        }
        [Required]
        [StringLength(150)]
        public string Address { get; set; }
        [NotMapped]
        public string AddressED
        {
            get { return passwordHelper.AesDecryption(Address); }
            set { }
        }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime Dob { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [Display(Name = "Image")]
        [StringLength(250)]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("IsEmailUnique", "Account", ErrorMessage = "This Email Already Registread")]
        public string Email { get; set; }
        [NotMapped]
        public string EmailED
        {
            get { return passwordHelper.AesDecryption(Email); }
            set { }
        }
        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        public int RoleId { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime Joined { get; set; }

        public virtual RoleTB RoleTB { get; set; }
    }
}
