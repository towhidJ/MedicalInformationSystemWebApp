namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PatientReport")]
    public partial class PatientReport
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        [StringLength(400)]
        public string PrescribeMedecine { get; set; }

        [StringLength(50)]
        public string PrecribeSurgery { get; set; }

        public virtual PatientTB PatientTB { get; set; }
    }
}
