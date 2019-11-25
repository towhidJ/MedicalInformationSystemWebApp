using System.Web.Mvc;

namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WardTB")]
    public partial class WardTB
    {
        PasswordHelper passwordHelper = new PasswordHelper();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WardTB()
        {
            PatientTBs = new HashSet<PatientTB>();
            SeatTBs = new HashSet<SeatTB>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Remote("IswardUnique", "Ward", ErrorMessage = "This Ward Already Added")]
        public string WardNo { get; set; }

        [NotMapped]
        public string wwardno
        {
            get { return passwordHelper.AesDecryption(WardNo); }
            set { }
        }

        public int DepartmentId { get; set; }

        public int SeatQuentity { get; set; }

        public int AvailableSeat { get; set; }
        [Required]
        [Display(Name = "Total Seat")]
        public int TotalSeat { get; set; }

        public virtual DepartmentTB DepartmentTB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientTB> PatientTBs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeatTB> SeatTBs { get; set; }
    }
}