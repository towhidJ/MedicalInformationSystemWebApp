namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PrescribeTestTB")]
    public partial class PrescribeTestTB
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int? RefferDoctorId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        public string TestName { get; set; }

        public virtual DoctorTB DoctorTB { get; set; }

        public virtual PatientTB PatientTB { get; set; }
    }
}
