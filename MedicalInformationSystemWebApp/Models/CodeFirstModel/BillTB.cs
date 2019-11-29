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

        [StringLength(300)]
        public string BillNo { get; set; }

        public int DoctorFee { get; set; }

        public int MedicalFee { get; set; }

        public int? Testfee { get; set; }

        public int TotalAmmount { get; set; }

        public virtual PatientTB PatientTB { get; set; }
    }
}
