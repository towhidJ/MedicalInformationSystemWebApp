namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestRepaortTB")]
    public partial class TestRepaortTB
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Report { get; set; }

        public int? TestId { get; set; }

        public virtual TestTB TestTB { get; set; }
    }
}
