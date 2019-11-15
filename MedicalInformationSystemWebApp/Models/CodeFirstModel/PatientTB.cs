namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PatientTB")]
    public partial class PatientTB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PatientTB()
        {
            BillTBs = new HashSet<BillTB>();
            PatientReports = new HashSet<PatientReport>();
            PrescribeTestTBs = new HashSet<PrescribeTestTB>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int Age { get; set; }

        public int Action { get; set; }

        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int NurseId { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime AdmitDate { get; set; }

        [Required]
        [Display(Name ="Ward")]
        public int WardId { get; set; }

        [Required]
        [Display(Name = "Seat")]
        public int SeatId { get; set; }

        [StringLength(250)]
        public string Problem { get; set; }

        [StringLength(150)]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(7)]
        public string Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillTB> BillTBs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientReport> PatientReports { get; set; }

        public virtual SeatTB SeatTB { get; set; }

        public virtual WardTB WardTB { get; set; }

        public virtual DoctorTB DoctorTB { get; set; }  //new add

        public virtual NurseTB NurseTB { get; set; }  //new add

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrescribeTestTB> PrescribeTestTBs { get; set; }
    }
}
