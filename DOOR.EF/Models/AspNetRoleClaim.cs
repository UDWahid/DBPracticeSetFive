using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("ASP_NET_ROLE_CLAIMS")]
    [Index("RoleId", Name = "IX_ASP_NET_ROLE_CLAIMS_ROLE_ID")]
    public partial class AspNetRoleClaim
    {
        [Key]
        [Column("ID")]
        [Precision(10)]
        public int Id { get; set; }
        [Column("ROLE_ID")]
        public string RoleId { get; set; } = null!;
        [Column("CLAIM_TYPE")]
        public string? ClaimType { get; set; }
        [Column("CLAIM_VALUE")]
        public string? ClaimValue { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("AspNetRoleClaims")]
        public virtual AspNetRole Role { get; set; } = null!;
    }
}
