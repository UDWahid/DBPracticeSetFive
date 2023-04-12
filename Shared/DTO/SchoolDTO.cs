using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace DOOR.Shared.DTO
{
    public class SchoolDTO
    {

        [Precision(8)]
        public int SchoolId { get; set; }

        [StringLength(30)]
        [Unicode(false)]
        public string SchoolName { get; set; } = null!;

        [StringLength(30)]
        [Unicode(false)]
        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }

    }
}
