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
        PasswordHelper passwordHelper = new PasswordHelper();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PrescribeTestTB()
        {
            TestTBs = new HashSet<TestTB>();
        }

        public int Id { get; set; }

        public int PatientId { get; set; }

        public int? RefferDoctorId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(300)]
        public string TestName { get; set; }
        [NotMapped]
        public string TestNameED
        {
            get { return passwordHelper.AesDecryption(TestName); }
            set { }
        }
        [DataType(DataType.MultilineText)]
        [StringLength(300)]
        public string Midkit { get; set; }
        [NotMapped]
        public string MidkitED
        {
            get { return passwordHelper.AesDecryption(Midkit); }
            set { }
        }
        public virtual DoctorTB DoctorTB { get; set; }

        public virtual PatientTB PatientTB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestTB> TestTBs { get; set; }
    }
}
