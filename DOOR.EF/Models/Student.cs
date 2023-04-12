using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("STUDENT")]
    [Index("StudentId", Name = "STU_PK", IsUnique = true)]
    [Index("Zip", Name = "STU_ZIP_FK_I")]
    public partial class Student
    {
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        [Key]
        [Column("STUDENT_ID")]
        [Precision(8)]
        public int StudentId { get; set; }
        [Column("SALUTATION")]
        [StringLength(5)]
        [Unicode(false)]
        public string? Salutation { get; set; }
        [Column("FIRST_NAME")]
        [StringLength(25)]
        [Unicode(false)]
        public string? FirstName { get; set; }
        [Column("LAST_NAME")]
        [StringLength(25)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [Column("STREET_ADDRESS")]
        [StringLength(50)]
        [Unicode(false)]
        public string? StreetAddress { get; set; }
        [Column("ZIP")]
        [StringLength(5)]
        [Unicode(false)]
        public string Zip { get; set; } = null!;
        [Column("PHONE")]
        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [Column("EMPLOYER")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Employer { get; set; }
        [Column("REGISTRATION_DATE", TypeName = "DATE")]
        public DateTime RegistrationDate { get; set; }
        [Column("CREATED_BY")]
        [StringLength(30)]
        [Unicode(false)]
        public string CreatedBy { get; set; } = null!;
        [Column("CREATED_DATE", TypeName = "DATE")]
        public DateTime CreatedDate { get; set; }
        [Column("MODIFIED_BY")]
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        [Column("MODIFIED_DATE", TypeName = "DATE")]
        public DateTime ModifiedDate { get; set; }
        [Key]
        [Column("SCHOOL_ID")]
        [Precision(8)]
        public int SchoolId { get; set; }

        [ForeignKey("SchoolId")]
        [InverseProperty("Students")]
        public virtual School School { get; set; } = null!;
        [InverseProperty("SNavigation")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
