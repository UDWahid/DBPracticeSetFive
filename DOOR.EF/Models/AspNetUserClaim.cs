using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("ASP_NET_USER_CLAIMS")]
    [Index("UserId", Name = "IX_ASP_NET_USER_CLAIMS_USER_ID")]
    public partial class AspNetUserClaim
    {
        [Key]
        [Column("ID")]
        [Precision(10)]
        public int Id { get; set; }
        [Column("USER_ID")]
        public string UserId { get; set; } = null!;
        [Column("CLAIM_TYPE")]
        public string? ClaimType { get; set; }
        [Column("CLAIM_VALUE")]
        public string? ClaimValue { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("AspNetUserClaims")]
        public virtual AspNetUser User { get; set; } = null!;
    }
}
