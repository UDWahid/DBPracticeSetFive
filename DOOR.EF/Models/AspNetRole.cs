using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("ASP_NET_ROLES")]
    [Index("NormalizedName", Name = "RoleNameIndex", IsUnique = true)]
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            Users = new HashSet<AspNetUser>();
        }

        [Key]
        [Column("ID")]
        public string Id { get; set; } = null!;
        [Column("NAME")]
        [StringLength(256)]
        public string? Name { get; set; }
        [Column("NORMALIZED_NAME")]
        [StringLength(256)]
        public string? NormalizedName { get; set; }
        [Column("CONCURRENCY_STAMP")]
        public string? ConcurrencyStamp { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Roles")]
        public virtual ICollection<AspNetUser> Users { get; set; }
    }
}
