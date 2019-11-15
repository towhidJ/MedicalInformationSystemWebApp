namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestTB")]
    public partial class TestTB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestTB()
        {
            TestRepaortTBs = new HashSet<TestRepaortTB>();
        }

        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime TestDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime DeliveryDate { get; set; }

        public int PrescribeTestId { get; set; }

        public double TestFee { get; set; }

        public virtual PrescribeTestTB PrescribeTestTB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestRepaortTB> TestRepaortTBs { get; set; }
    }
}
