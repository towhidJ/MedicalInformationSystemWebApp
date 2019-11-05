namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StaffTB")]
    public partial class StaffTB
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime Dob { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column(TypeName = "date")]
        public DateTime? Joined { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        public string ImagePath { get; set; }

        public int RoleId { get; set; }

        [Required]
        [StringLength(14)]
        public string Phone { get; set; }

        [Required]
        [StringLength(7)]
        public string Gender { get; set; }

        public virtual RoleTB RoleTB { get; set; }
    }
}
