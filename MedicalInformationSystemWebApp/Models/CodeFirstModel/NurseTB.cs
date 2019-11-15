using System.Web.Mvc;

namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NurseTB")]
    public partial class NurseTB
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NurseTB()
        {
            PatientTBs = new HashSet<PatientTB>();  // new Add

        }

        [Key]
        public int NurseId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [Required]
        [StringLength(14)]
        public string Phone { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime Dob { get; set; }

        [Required]
        [StringLength(150)]
        public string Qualification { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime Joined { get; set; }

        [Display(Name = "Image")]
        [StringLength(250)]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("IsEmailUnique", "Account", ErrorMessage = "This Email Already Registread")]
        public string Email { get; set; }

        [Required]
        [StringLength(200, MinimumLength=6)]
        public string Password { get; set; }

        public int RoleId { get; set; }

        [Required]
        [StringLength(6)]
        public string Gender { get; set; }

        public virtual RoleTB RoleTB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientTB> PatientTBs { get; set; }
    }
}
