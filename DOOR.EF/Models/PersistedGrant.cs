using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("PERSISTED_GRANTS")]
    [Index("ConsumedTime", Name = "IX_PERSISTED_GRANTS_CONSUMED_TIME")]
    [Index("Expiration", Name = "IX_PERSISTED_GRANTS_EXPIRATION")]
    [Index("SubjectId", "ClientId", "Type", Name = "IX_PERSISTED_GRANTS_SUBJECT_ID_CLIENT_ID_TYPE")]
    [Index("SubjectId", "SessionId", "Type", Name = "IX_PERSISTED_GRANTS_SUBJECT_ID_SESSION_ID_TYPE")]
    public partial class PersistedGrant
    {
        [Key]
        [Column("KEY")]
        [StringLength(200)]
        public string Key { get; set; } = null!;
        [Column("TYPE")]
        [StringLength(50)]
        public string Type { get; set; } = null!;
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
        public DateTime? Expiration { get; set; }
        [Column("CONSUMED_TIME")]
        [Precision(7)]
        public DateTime? ConsumedTime { get; set; }
        [Column("DATA", TypeName = "NCLOB")]
        public string Data { get; set; } = null!;
    }
}
