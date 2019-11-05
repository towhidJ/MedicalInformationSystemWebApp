namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BillTB")]
    public partial class BillTB
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        [Required]
        [StringLength(50)]
        public string BillNo { get; set; }

        public double DoctorFee { get; set; }

        public double MedicalFee { get; set; }

        public double MedicineFee { get; set; }

        public double? Testfee { get; set; }

        public double TotalAmmount { get; set; }

        public virtual BillTB BillTB1 { get; set; }

        public virtual BillTB BillTB2 { get; set; }

        public virtual PatientTB PatientTB { get; set; }
    }
}
