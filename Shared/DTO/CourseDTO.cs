using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DOOR.Shared.DTO
{
    public class CourseDTO
    {
        public int CourseNo { get; set; }
        [StringLength(50)]
        public string Description { get; set; } = null!;
        public decimal? Cost { get; set; }
        public int? Prerequisite { get; set; }
        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]

        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

        public int SchoolId { get; set; }
        public int? PrerequisiteSchoolId { get; set; }
    }
}
