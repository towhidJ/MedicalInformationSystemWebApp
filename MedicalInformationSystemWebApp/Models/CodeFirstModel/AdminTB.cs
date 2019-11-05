using System.Web.Mvc;

namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminTB")]
    public partial class AdminTB
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("IsEmailUnique", "Account", ErrorMessage = "This Email Already Registread")]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public virtual RoleTB RoleTB { get; set; }
    }
}
