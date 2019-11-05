namespace MedicalInformationSystemWebApp.Models.CodeFirstModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MedicalInfoSys : DbContext
    {
        public MedicalInfoSys()
            : base("name=MedicalInfoSys")
        {
        }

        public virtual DbSet<AdminTB> AdminTBs { get; set; }
        public virtual DbSet<AppointmentTB> AppointmentTBs { get; set; }
        public virtual DbSet<BillTB> BillTBs { get; set; }
        public virtual DbSet<DepartmentTB> DepartmentTBs { get; set; }
        public virtual DbSet<DesignationTB> DesignationTBs { get; set; }
        public virtual DbSet<DoctorTB> DoctorTBs { get; set; }
        public virtual DbSet<NurseTB> NurseTBs { get; set; }
        public virtual DbSet<PatientReport> PatientReports { get; set; }
        public virtual DbSet<PatientTB> PatientTBs { get; set; }
        public virtual DbSet<PrescribeTestTB> PrescribeTestTBs { get; set; }
        public virtual DbSet<ReceptionTB> ReceptionTBs { get; set; }
        public virtual DbSet<RoleTB> RoleTBs { get; set; }
        public virtual DbSet<SeatTB> SeatTBs { get; set; }
        public virtual DbSet<SpealizationTB> SpealizationTBs { get; set; }
        public virtual DbSet<StaffTB> StaffTBs { get; set; }
        public virtual DbSet<TestRepaortTB> TestRepaortTBs { get; set; }
        public virtual DbSet<TestTB> TestTBs { get; set; }
        public virtual DbSet<WardTB> WardTBs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminTB>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AdminTB>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<AdminTB>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<AppointmentTB>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AppointmentTB>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<AppointmentTB>()
                .Property(e => e.AppointmentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<BillTB>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<BillTB>()
                .HasOptional(e => e.BillTB1)
                .WithRequired(e => e.BillTB2);

            modelBuilder.Entity<DepartmentTB>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentTB>()
                .HasMany(e => e.DoctorTBs)
                .WithRequired(e => e.DepartmentTB)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DepartmentTB>()
                .HasMany(e => e.WardTBs)
                .WithRequired(e => e.DepartmentTB)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DesignationTB>()
                .Property(e => e.DesignationName)
                .IsUnicode(false);

            modelBuilder.Entity<DesignationTB>()
                .HasMany(e => e.DoctorTBs)
                .WithRequired(e => e.DesignationTB)
                .HasForeignKey(e => e.DesignationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.Password)
                .IsUnicode(false);

            //modelBuilder.Entity<DoctorTB>()
            //    .Property(e => e.VisitingTimeStart)
            //    .HasPrecision(0);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.VisitingTimeStart)
                .IsUnicode(false);

            //modelBuilder.Entity<DoctorTB>()
            //    .Property(e => e.VisitingTimeEnd)
            //    .HasPrecision(0);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.VisitingTimeEnd)
                .IsUnicode(false);

            modelBuilder.Entity<DoctorTB>()
                .Property(e => e.VisitDay)
                .IsUnicode(false);

            //modelBuilder.Entity<DoctorTB>()
            //    .HasMany(e => e.VisitDay)
            //    .WithMany();

            modelBuilder.Entity<DoctorTB>()
                .HasMany(e => e.PrescribeTestTBs)
                .WithOptional(e => e.DoctorTB)
                .HasForeignKey(e => e.RefferDoctorId);

            modelBuilder.Entity<DoctorTB>()
                .HasMany(e => e.AppointmentTBs)
                .WithRequired(e => e.DoctorTB)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NurseTB>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<NurseTB>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<NurseTB>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<NurseTB>()
                .Property(e => e.Qualification)
                .IsUnicode(false);

            modelBuilder.Entity<NurseTB>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<NurseTB>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NurseTB>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<NurseTB>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<PatientReport>()
                .Property(e => e.PrescribeMedecine)
                .IsUnicode(false);

            modelBuilder.Entity<PatientReport>()
                .Property(e => e.PrecribeSurgery)
                .IsUnicode(false);

            modelBuilder.Entity<PatientTB>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PatientTB>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<PatientTB>()
                .Property(e => e.Problem)
                .IsUnicode(false);

            modelBuilder.Entity<PatientTB>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<PatientTB>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<PatientTB>()
                .HasMany(e => e.BillTBs)
                .WithRequired(e => e.PatientTB)
                .HasForeignKey(e => e.PatientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PatientTB>()
                .HasMany(e => e.PatientReports)
                .WithRequired(e => e.PatientTB)
                .HasForeignKey(e => e.PatientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PatientTB>()
                .HasMany(e => e.PrescribeTestTBs)
                .WithRequired(e => e.PatientTB)
                .HasForeignKey(e => e.PatientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrescribeTestTB>()
                .Property(e => e.TestName)
                .IsUnicode(false);

            modelBuilder.Entity<ReceptionTB>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ReceptionTB>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<ReceptionTB>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<ReceptionTB>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<ReceptionTB>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<ReceptionTB>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ReceptionTB>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<RoleTB>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<RoleTB>()
                .HasMany(e => e.AdminTBs)
                .WithRequired(e => e.RoleTB)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleTB>()
                .HasMany(e => e.DoctorTBs)
                .WithOptional(e => e.RoleTB)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<RoleTB>()
                .HasMany(e => e.NurseTBs)
                .WithRequired(e => e.RoleTB)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleTB>()
                .HasMany(e => e.ReceptionTBs)
                .WithRequired(e => e.RoleTB)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleTB>()
                .HasMany(e => e.StaffTBs)
                .WithRequired(e => e.RoleTB)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SeatTB>()
                .HasMany(e => e.PatientTBs)
                .WithRequired(e => e.SeatTB)
                .HasForeignKey(e => e.SeatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SeatTB>()
                .Property(e => e.SeatNo)
                .IsUnicode(false);

            modelBuilder.Entity<SpealizationTB>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<SpealizationTB>()
                .HasMany(e => e.DoctorTBs)
                .WithRequired(e => e.SpealizationTB)
                .HasForeignKey(e => e.SpealizationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffTB>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<StaffTB>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<StaffTB>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<StaffTB>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<StaffTB>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<TestRepaortTB>()
                .Property(e => e.Report)
                .IsUnicode(false);

            modelBuilder.Entity<TestTB>()
                .HasMany(e => e.TestRepaortTBs)
                .WithOptional(e => e.TestTB)
                .HasForeignKey(e => e.TestId);

            modelBuilder.Entity<WardTB>()
                .Property(e => e.WardNo)
                .IsUnicode(false);

            modelBuilder.Entity<WardTB>()
                .HasMany(e => e.PatientTBs)
                .WithRequired(e => e.WardTB)
                .HasForeignKey(e => e.WardId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WardTB>()
                .HasMany(e => e.SeatTBs)
                .WithRequired(e => e.WardTB)
                .HasForeignKey(e => e.WardId)
                .WillCascadeOnDelete(false);
        }
    }
}
