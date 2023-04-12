using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("ASP_NET_USER_LOGINS")]
    [Index("UserId", Name = "IX_ASP_NET_USER_LOGINS_USER_ID")]
    public partial class AspNetUserLogin
    {
        [Key]
        [Column("LOGIN_PROVIDER")]
        [StringLength(128)]
        public string LoginProvider { get; set; } = null!;
        [Key]
        [Column("PROVIDER_KEY")]
        [StringLength(128)]
        public string ProviderKey { get; set; } = null!;
        [Column("PROVIDER_DISPLAY_NAME")]
        public string? ProviderDisplayName { get; set; }
        [Column("USER_ID")]
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("AspNetUserLogins")]
        public virtual AspNetUser User { get; set; } = null!;
    }
}
