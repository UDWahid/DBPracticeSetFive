using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("ASP_NET_USER_TOKENS")]
    public partial class AspNetUserToken
    {
        [Key]
        [Column("USER_ID")]
        public string UserId { get; set; } = null!;
        [Key]
        [Column("LOGIN_PROVIDER")]
        [StringLength(128)]
        public string LoginProvider { get; set; } = null!;
        [Key]
        [Column("NAME")]
        [StringLength(128)]
        public string Name { get; set; } = null!;
        [Column("VALUE")]
        public string? Value { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("AspNetUserTokens")]
        public virtual AspNetUser User { get; set; } = null!;
    }
}
