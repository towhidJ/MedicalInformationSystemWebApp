using System.Web.Mvc;

namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoctorTB")]
    public partial class DoctorTB
    {
        PasswordHelper passwordHelper = new PasswordHelper();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoctorTB()
        {
            PrescribeTestTBs = new HashSet<PrescribeTestTB>();
            AppointmentTBs = new HashSet<AppointmentTB>();
            PatientTBs = new HashSet<PatientTB>();   //new add

        }

        [Key]
        public int DoctorId { get; set; }

        public int DepartmentId { get; set; }

        public int SpealizationId { get; set; }

        public int DesignationId { get; set; }

        public int? RoleId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [NotMapped]
        public string NameED
        {
            get { return passwordHelper.AesDecryption(Name); }
            set { }
        }

        [StringLength(250)]
        public string Address { get; set; }
        [NotMapped]
        public string AddressED
        {
            get { return passwordHelper.AesDecryption(Address); }
            set { }
        }

        [StringLength(250)]
        public string Phone { get; set; }
        [NotMapped]
        public string PhoneED
        {
            get { return passwordHelper.AesDecryption(Phone); }
            set { }
        }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime Dob { get; set; }

        [Required]
        [StringLength(6)]
        public string Gender { get; set; }

        [Display(Name = "Image")]
        [StringLength(250)]
        public string ImagePath { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime Joined { get; set; }

        [Required]
        [StringLength(200)]
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

        [StringLength(200)]
        public string VisitDay { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "From")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:tt}", ApplyFormatInEditMode = true)]
        public string VisitingTimeStart { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "To")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:tt}", ApplyFormatInEditMode = true)]
        public string VisitingTimeEnd { get; set; }



        public virtual DepartmentTB DepartmentTB { get; set; }

        public virtual DesignationTB DesignationTB { get; set; }

        public virtual RoleTB RoleTB { get; set; }

        public virtual SpealizationTB SpealizationTB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrescribeTestTB> PrescribeTestTBs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentTB> AppointmentTBs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientTB> PatientTBs { get; set; }
    }
}
