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
        PasswordHelper passwordHelper = new PasswordHelper();
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(300)]
        public string Report { get; set; }
        [NotMapped]
        public string ReportED
        {
            get { return passwordHelper.AesDecryption(Report); }
            set { }
        }
        public int? TestId { get; set; }

        public virtual TestTB TestTB { get; set; }
    }
}
