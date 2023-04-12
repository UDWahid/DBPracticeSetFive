using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("DEVICE_CODES")]
    [Index("DeviceCode1", Name = "IX_DEVICE_CODES_DEVICE_CODE", IsUnique = true)]
    [Index("Expiration", Name = "IX_DEVICE_CODES_EXPIRATION")]
    public partial class DeviceCode
    {
        [Key]
        [Column("USER_CODE")]
        [StringLength(200)]
        public string UserCode { get; set; } = null!;
        [Column("DEVICE_CODE")]
        [StringLength(200)]
        public string DeviceCode1 { get; set; } = null!;
        [Column("SUBJECT_ID")]
        [StringLength(200)]
        public string? SubjectId { get; set; }
        [Column("SESSION_ID")]
        [StringLength(100)]
        public string? SessionId { get; set; }
        [Column("CLIENT_ID")]
        [StringLength(200)]
        public string ClientId { get; set; } = null!;
        [Column("DESCRIPTION")]
        [StringLength(200)]
        public string? Description { get; set; }
        [Column("CREATION_TIME")]
        [Precision(7)]
        public DateTime CreationTime { get; set; }
        [Column("EXPIRATION")]
        [Precision(7)]
        public DateTime Expiration { get; set; }
        [Column("DATA", TypeName = "NCLOB")]
        public string Data { get; set; } = null!;
    }
}
