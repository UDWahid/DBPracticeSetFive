using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("COURSE")]
    [Index("Prerequisite", Name = "CRSE_CRSE_FK_I")]
    [Index("CourseNo", Name = "CRSE_PK", IsUnique = true)]
    public partial class Course
    {
        public Course()
        {
            InversePrerequisiteNavigation = new HashSet<Course>();
            Sections = new HashSet<Section>();
        }

        [Key]
        [Column("COURSE_NO")]
        [Precision(8)]
        public int CourseNo { get; set; }
        [Column("DESCRIPTION")]
        [StringLength(50)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
        [Column("COST", TypeName = "NUMBER(9,2)")]
        public decimal? Cost { get; set; }
        [Column("PREREQUISITE")]
        [Precision(8)]
        public int? Prerequisite { get; set; }
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
        [Column("PREREQUISITE_SCHOOL_ID")]
        [Precision(8)]
        public int? PrerequisiteSchoolId { get; set; }

        [ForeignKey("Prerequisite,PrerequisiteSchoolId")]
        [InverseProperty("InversePrerequisiteNavigation")]
        public virtual Course? PrerequisiteNavigation { get; set; }
        [ForeignKey("SchoolId")]
        [InverseProperty("Courses")]
        public virtual School School { get; set; } = null!;
        [InverseProperty("PrerequisiteNavigation")]
        public virtual ICollection<Course> InversePrerequisiteNavigation { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<Section> Sections { get; set; }
    }
}
