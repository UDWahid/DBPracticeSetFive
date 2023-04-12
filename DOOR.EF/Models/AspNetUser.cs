using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("ASP_NET_USERS")]
    [Index("NormalizedEmail", Name = "EmailIndex")]
    [Index("NormalizedUserName", Name = "UserNameIndex", IsUnique = true)]
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            Roles = new HashSet<AspNetRole>();
        }

        [Key]
        [Column("ID")]
        public string Id { get; set; } = null!;
        [Column("USER_NAME")]
        [StringLength(256)]
        public string? UserName { get; set; }
        [Column("NORMALIZED_USER_NAME")]
        [StringLength(256)]
        public string? NormalizedUserName { get; set; }
        [Column("EMAIL")]
        [StringLength(256)]
        public string? Email { get; set; }
        [Column("NORMALIZED_EMAIL")]
        [StringLength(256)]
        public string? NormalizedEmail { get; set; }
        [Column("EMAIL_CONFIRMED")]
        [Precision(1)]
        public bool EmailConfirmed { get; set; }
        [Column("PASSWORD_HASH")]
        public string? PasswordHash { get; set; }
        [Column("SECURITY_STAMP")]
        public string? SecurityStamp { get; set; }
        [Column("CONCURRENCY_STAMP")]
        public string? ConcurrencyStamp { get; set; }
        [Column("PHONE_NUMBER")]
        public string? PhoneNumber { get; set; }
        [Column("PHONE_NUMBER_CONFIRMED")]
        [Precision(1)]
        public bool PhoneNumberConfirmed { get; set; }
        [Column("TWO_FACTOR_ENABLED")]
        [Precision(1)]
        public bool TwoFactorEnabled { get; set; }
        [Column("LOCKOUT_END")]
        [Precision(7)]
        public DateTimeOffset? LockoutEnd { get; set; }
        [Column("LOCKOUT_ENABLED")]
        [Precision(1)]
        public bool LockoutEnabled { get; set; }
        [Column("ACCESS_FAILED_COUNT")]
        [Precision(10)]
        public int AccessFailedCount { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Users")]
        public virtual ICollection<AspNetRole> Roles { get; set; }
    }
}
