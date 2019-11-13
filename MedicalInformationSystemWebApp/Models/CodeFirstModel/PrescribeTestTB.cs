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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PrescribeTestTB()
        {
            TestTBs = new HashSet<TestTB>();
        }

        public int Id { get; set; }

        public int PatientId { get; set; }

        public int? RefferDoctorId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        public string TestName { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(300)]
        public string Midkit { get; set; }

        public virtual DoctorTB DoctorTB { get; set; }

        public virtual PatientTB PatientTB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestTB> TestTBs { get; set; }
    }
}
