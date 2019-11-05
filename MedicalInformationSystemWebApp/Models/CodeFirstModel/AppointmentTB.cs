using System.Dynamic;

namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppointmentTB")]
    public partial class AppointmentTB
    {

        [Key]
        public int AppointmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [Display(Name="Doctor")]
        public int DoctorId { get; set; }

        public int? Age { get; set; }

        public double? AppointmentFee { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime AppointmentDate { get; set; }

        [StringLength(100)]
        public string AppointmentNumber { get; set; }

        public virtual DoctorTB DoctorTB { get; set; }

    }
}
