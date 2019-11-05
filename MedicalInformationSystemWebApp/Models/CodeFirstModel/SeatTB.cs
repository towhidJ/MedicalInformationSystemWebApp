using System.Web.Mvc;

namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeatTB")]
    public partial class SeatTB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SeatTB()
        {
            PatientTBs = new HashSet<PatientTB>();
        }

        public int Id { get; set; }

        [Display(Name = "Ward No")]
        public int WardId { get; set; }

        [Required]
        [Display(Name = "Seat No")]
        [Remote("IsSeatUnique", "Seat", ErrorMessage = "This Seat Number Already Added")]
        public string SeatNo { get; set; }
        //public int SeatQuentity { get; set; }

        //public int AvailableSeat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientTB> PatientTBs { get; set; }

        public virtual WardTB WardTB { get; set; }
    }
}
